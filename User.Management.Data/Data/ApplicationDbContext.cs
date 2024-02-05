using FlashFood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Management.Data.Data;

namespace FlashFood.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
            builder.Seed();

            builder.Entity<Restaurant>()
                .HasOne(r => r.Menu)
                .WithOne(m => m.Restaurant)
                .HasForeignKey<Menu>(m => m.RestaurantId);

            builder.Entity<Restaurant>()
                .HasMany(r => r.Feedbacks)
                .WithOne(f => f.Restaurant)
                .HasForeignKey(f => f.RestaurantId);

            builder.Entity<Menu>()
                .HasMany(m => m.Dishes)
                .WithOne(d => d.Menu)
                .HasForeignKey(d => d.MenuId);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .IsRequired();

            builder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.DishId });

            builder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            builder.Entity<OrderDetail>()
                .HasOne(od => od.Dish)
                .WithMany(d => d.OrderDetails)
                .HasForeignKey(od => od.DishId);

            builder.Entity<Dish>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            builder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasPrecision(10, 2);
        }
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                    new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"}
                );
        }
    }
}
