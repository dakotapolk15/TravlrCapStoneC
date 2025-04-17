using Microsoft.EntityFrameworkCore;
using travlr.Backend.Models;

namespace travlr.Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }  
        public DbSet<Trip> Trips { get; set; }
    }
}
