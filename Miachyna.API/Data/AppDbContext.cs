using Miachyna.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Miachyna.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cosmetic> Cosmetics { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
