using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;
using User.Management.Data.Data.DTOs;

namespace User.Management.Service.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<List<Order>> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(Order order);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
    }
}
