using FlashFood.Models;
using User.Management.Data.Data.DTOs;
using User.Management.Data.Data;
using User.Management.Service.Models;
using AutoMapper;
using User.Management.Service.Repositories.DishRepository;
using User.Management.Service.Repositories.OrderRepository;
using User.Management.Service.Repositories.RestaurantRepository;

namespace User.Management.Service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public OrderService(IRestaurantRepository restaurantRepository,
                            IOrderRepository orderRepository,
                            IMapper mapper,
                            IDishRepository dishRepository) 
        {
            _restaurantRepository = restaurantRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _dishRepository = dishRepository;
        }
        public async Task<Order> CreateOrderAsync(ApplicationUser user, CreateOrderModel orderModel)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(orderModel.RestaurantId);
            if (restaurant == null)
            {
                throw new Exception("Restaurant not found.");
            }

            var dishes = await _dishRepository.GetDishesByIdAsync(orderModel.DishIds);
            if (dishes.Count != orderModel.DishIds.Count)
            {
                throw new Exception("One or more dishes not found.");
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                PlacedDate = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var dish in dishes)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    DishId = dish.Id,
                    OrderId = order.Id,
                    Dish = dish,
                    Order = order
                });
                order.TotalPrice += dish.Price;
            }
            await _orderRepository.AddAsync(order);
            return order;
        }
        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetUserOrdersAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}
