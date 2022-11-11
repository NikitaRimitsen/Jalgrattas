using Jalgratta.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jalgratta.Models
{
    public class DataBase : DbContext
    {
        public DbSet<Teenus>? Teenus { get; set; }
        public DbSet<Tootajad>? Tootajad { get; set; }
        public DbSet<Kasutaja>? Kasutaja { get; set; }
        public DbSet<Teenusetelimus>? Teenusetelimus { get; set; }

        public DataBase(DbContextOptions<DataBase> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
