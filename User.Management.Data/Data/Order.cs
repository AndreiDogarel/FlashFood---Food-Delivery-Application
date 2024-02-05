using User.Management.Data.Data;

namespace FlashFood.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PlacedDate { get; set;}
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser? User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; } = null!;
    }
}
