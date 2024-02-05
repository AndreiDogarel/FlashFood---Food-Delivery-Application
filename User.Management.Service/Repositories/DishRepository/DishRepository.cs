using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Data;
using FlashFood.Models;
using Microsoft.EntityFrameworkCore;

namespace User.Management.Service.Repositories.DishRepository
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _context;
        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Dish>> GetDishesByIdAsync(List<Guid> ids)
        {
            return await _context.Dishes
                             .Where(d => ids.Contains(d.Id))
                             .ToListAsync();
        }
    }
}
