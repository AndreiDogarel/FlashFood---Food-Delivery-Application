using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Management.Data.Data.DTOs;
using User.Management.Service.Models;

namespace User.Management.Service.Services.RestaurantService
{
    public interface IRestaurantService
    {
        Task<List<RestaurantDto>> SearchRestaurantsAsync(string keyword);
        Task<List<RestaurantDto>> GetAllAsync();
        Task<bool> DeleteRestaurantAsync(Guid restaurantId);
        Task CreateRestaurantAsync(CreateRestaurantModel restaurantModel);
    }
}
