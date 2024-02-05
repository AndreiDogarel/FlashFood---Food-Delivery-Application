using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;

namespace User.Management.Service.Repositories.DishRepository
{
    public interface IDishRepository
    {
        Task<List<Dish>> GetDishesByIdAsync(List<Guid> ids);
    }
}
