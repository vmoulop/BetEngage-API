using Microsoft.EntityFrameworkCore;

using BetEngage.Api.Models;

namespace BetEngage.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}





