using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;

namespace User.Management.Data.Data
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public int Rating { get; set; }
        public string? Description { get; set; }
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
