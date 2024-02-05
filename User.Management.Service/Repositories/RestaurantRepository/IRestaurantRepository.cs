using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;
using User.Management.Data.Data;
using User.Management.Data.Data.DTOs;

namespace User.Management.Service.Repositories.RestaurantRepository
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAllAsync(int pageNumber, int pageSize);
        Task<Restaurant> GetByIdAsync(Guid id);
        Task AddAsync(Restaurant restaurant);
        Task<List<Restaurant>> SearchAsync(string keyword);
        Task DeleteAsync(Restaurant restaurant);
    }
}
