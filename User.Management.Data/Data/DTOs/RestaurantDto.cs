using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;

namespace User.Management.Data.Data.DTOs
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual MenuDto? Menu { get; set; }
        public virtual List<FeedbackDto>? Feedbacks { get; set; }
    }
}
