namespace FlashFood.Models
{
    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public Guid DishId { get; set; }
        public virtual Dish Dish { get; set; } = null!;
    }
}
