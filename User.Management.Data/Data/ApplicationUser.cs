using FlashFood.Models;
using Microsoft.AspNetCore.Identity;

namespace User.Management.Data.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
}
