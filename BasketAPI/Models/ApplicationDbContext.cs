using Microsoft.EntityFrameworkCore;

namespace BasketAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Basket> Baskets { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
    }
}
