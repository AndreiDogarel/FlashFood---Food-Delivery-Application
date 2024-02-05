using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FlashFood.Data;
using FlashFood.Models;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using User.Management.Data.Data.DTOs;

namespace User.Management.Service.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var skipAmount = pageSize * (pageNumber - 1);
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Dish)
                .OrderBy(o => o.Id)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Dish)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Dish)
                .ToListAsync();

            return orders;
        }
    }
}
