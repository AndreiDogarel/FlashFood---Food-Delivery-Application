using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Management.Service.Models
{
    public class CreateFeedbackModel
    {
        public string UserName { get; set; } = null!;
        public int Rating { get; set; }
        public string? Description { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
