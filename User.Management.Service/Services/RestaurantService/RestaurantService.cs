using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FlashFood.Models;
using User.Management.Data.Data.DTOs;
using User.Management.Service.Models;
using User.Management.Service.Repositories.RestaurantRepository;

namespace User.Management.Service.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<List<RestaurantDto>> SearchRestaurantsAsync(string keyword)
        {
            var restaurants = await _restaurantRepository.SearchAsync(keyword);
            return _mapper.Map<List<RestaurantDto>>(restaurants);
        }
        public async Task<List<RestaurantDto>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync(1, 10);
            return _mapper.Map<List<RestaurantDto>>(restaurants);
        }
        public async Task<bool> DeleteRestaurantAsync(Guid restaurantId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return false;
            }

            await _restaurantRepository.DeleteAsync(restaurant);
            return true;
        }
        public async Task CreateRestaurantAsync(CreateRestaurantModel restaurantModel)
        {
            var restaurantId = Guid.NewGuid();
            var restaurant = new Restaurant
            {
                Id = restaurantId,
                Name = restaurantModel.Name,
                Menu = new Menu
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = restaurantId,
                    Dishes = restaurantModel.Dishes
                }
            };
            await _restaurantRepository.AddAsync(restaurant);
        }
    }
}
