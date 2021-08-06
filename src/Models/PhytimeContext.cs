using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Phytime.Models
{
    public class PhytimeContext : IdentityDbContext<User>
    {
        public DbSet<Feed> Feeds { get; set; }
        public PhytimeContext(DbContextOptions<PhytimeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
