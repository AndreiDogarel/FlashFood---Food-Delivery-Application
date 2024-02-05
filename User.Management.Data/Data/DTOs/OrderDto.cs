using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Management.Data.Data.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime PlacedDate { get; set; }
        public virtual List<DishDto> Dishes { get; set; }
    }
}
