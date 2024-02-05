using AutoMapper;
using FlashFood.Data;
using FlashFood.Models;
using Microsoft.EntityFrameworkCore;
using User.Management.Data.Data;
using User.Management.Data.Data.DTOs;

namespace User.Management.Service.Repositories.RestaurantRepository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationDbContext _context;
        public RestaurantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Restaurant>> GetAllAsync(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * (pageNumber - 1);
            var restaurants = await _context.Restaurants
                .Include(r => r.Menu)
                    .ThenInclude(m => m.Dishes)
                .Include(r => r.Feedbacks)
                .OrderBy(r => r.Id)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();

            return restaurants;
        }
        public async Task<Restaurant> GetByIdAsync(Guid id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Menu)
                    .ThenInclude(m => m.Dishes)
                .Include(r => r.Feedbacks)
                .OrderBy(r => r.Id)
                .FirstOrDefaultAsync();

            return restaurant;
        }
        public async Task<List<Restaurant>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _context.Restaurants
                    .Include(r => r.Menu)
                    .ToListAsync();
            }
            return await _context.Restaurants
                .Include(r => r.Menu)
                .ThenInclude(menu => menu.Dishes)
                .Where(r => r.Name.Contains(keyword)
                            || r.Menu.Dishes.Any(dish => dish.Name.Contains(keyword)))
                .ToListAsync();
        }
        public async Task DeleteAsync(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
