
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using travlr.Backend.Models;

namespace travlr.Backend.Data
{
    public class TravlrDbContext : DbContext
    {
        public TravlrDbContext(DbContextOptions<TravlrDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
    }
}
