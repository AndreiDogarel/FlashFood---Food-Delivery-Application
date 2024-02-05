using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;
using User.Management.Data.Data.DTOs;
using User.Management.Data.Data;
using User.Management.Service.Models;

namespace User.Management.Service.Services.OrderService
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(ApplicationUser user, CreateOrderModel order);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId);
    }
}
