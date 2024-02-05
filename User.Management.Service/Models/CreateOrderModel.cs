using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Management.Service.Models
{
    public class CreateOrderModel
    {
        public string UserName { get; set; } = null!;
        public Guid RestaurantId { get; set; }
        public List<Guid> DishIds { get; set; } = null!;
    }
}
