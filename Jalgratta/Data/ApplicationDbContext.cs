using Jalgratta.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jalgratta.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Teenus>? Teenus { get; set; }
        public DbSet<Tootajad>? Tootajad { get; set; }
        public DbSet<Kasutaja>? Kasutaja { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}