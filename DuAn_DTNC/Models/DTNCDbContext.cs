using Microsoft.EntityFrameworkCore;
namespace DuAn_DTNC.Models
 
{
    public class DTNCDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ComboFoodItem> ComboFoodItems { get; set; }
        public DTNCDbContext(DbContextOptions<DTNCDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComboFoodItem>()
                .HasKey(cf => new { cf.ComboId, cf.FoodItemId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
