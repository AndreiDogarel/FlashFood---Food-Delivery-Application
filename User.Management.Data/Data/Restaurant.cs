using User.Management.Data.Data;

namespace FlashFood.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
        public virtual List<Feedback>? Feedbacks { get; set; }
    }
}
