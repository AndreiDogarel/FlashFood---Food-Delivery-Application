using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;

namespace User.Management.Data.Data.DTOs
{
    public class MenuDto
    {
        public virtual List<DishDto> Dishes { get; set; } = null!;
    }
}
