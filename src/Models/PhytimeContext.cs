using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Phytime.Models
{
    public class PhytimeContext : IdentityDbContext<User>
    {
        public DbSet<Feed.Feed> Feeds { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public PhytimeContext(DbContextOptions<PhytimeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recommendation>().HasData(
                new Recommendation
                {
                    Id = 1,
                    Author = "Литвинович Юлия",
                    Level = "начинающий",
                    PublishDate = "12/12/21",
                    Summary = "Здесь может быть ваша реклама",
                    Title = "Здесь реально может быть ваша реклама, и я до сих пор не понимаю почему её тут нет",
                },
                new Recommendation
                {
                    Id = 2,
                    Author = "Литвинович Юлия",
                    Level = "начинающий",
                    PublishDate = "12/12/21",
                    Summary = "Здесь может быть ваша реклама",
                    Title = "Здесь реально может быть ваша реклама, и я до сих пор не понимаю почему её тут нет",
                }
            );
        }
    }
}