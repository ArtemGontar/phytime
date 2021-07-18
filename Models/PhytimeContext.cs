using Microsoft.EntityFrameworkCore;

namespace Phytime.Models
{
    public class PhytimeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public PhytimeContext(DbContextOptions<PhytimeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
