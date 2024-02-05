namespace FlashFood.Models
{
    public class Menu
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
