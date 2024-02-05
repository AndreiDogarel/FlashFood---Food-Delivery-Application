using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;

namespace User.Management.Service.Models
{
    public class CreateRestaurantModel
    {
        public string Name { get; set; } = null!;
        public List<Dish> Dishes { get; set; }
    }
}
