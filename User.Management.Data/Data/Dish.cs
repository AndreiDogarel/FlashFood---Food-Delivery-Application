namespace FlashFood.Models
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public Guid MenuId { get; set; }
        public virtual Menu? Menu { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; } = null!;
    }
}
