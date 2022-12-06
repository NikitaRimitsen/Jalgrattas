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
        public DbSet<Teenusetelimuse>? Teenusetelimuse { get; set; }
        public DbSet<Teenusetelimuskasutaja>? Teenusetelimuskasutaja { get; set; }

        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kasutaja>().HasData(
                new Kasutaja
                {
                    KasutajaId = 1,
                    Nimi = "Nikita",
                    Perekonnanimi = "Rimitsen",
                    Email = "nikita@gmail.com",
                    Vanus = 18,
                    telnumber = "+372 7894 4897"
                },
                new Kasutaja
                {
                    KasutajaId = 2,
                    Nimi = "Aleksei",
                    Perekonnanimi = "Tiora",
                    Email = "aleks@gmail.com",
                    Vanus = 18,
                    telnumber = "+372 1111 7777"
                }
                );
            modelBuilder.Entity<Teenus>().HasData(
                new Teenus{TeenusId = 1,Info = "Keti vahetus",Hind = 8},
                new Teenus{TeenusId = 2,Info = "Rataste sirgendamine ", Hind = 10},
                new Teenus { TeenusId = 3, Info = "Rooli paigaldus või vahetus", Hind = 15 },
                new Teenus { TeenusId = 4, Info = "Tiibade paigaldus", Hind = 10 },
                new Teenus { TeenusId = 5, Info = "Piduriklotside vahetus (1 paar)", Hind = 6 },
                new Teenus { TeenusId = 6, Info = "Pidurite reguleerimine", Hind = 7 },
                new Teenus { TeenusId = 7, Info = "Spidomeetri paigaldus", Hind = 6 },
                new Teenus { TeenusId = 8, Info = "Eesmise käiguvahetaja reguleerimine", Hind = 6 },
                new Teenus { TeenusId = 9, Info = "Hooldus Lite", Hind = 40 },
                new Teenus { TeenusId = 10, Info = "Hooldus Premium", Hind = 89 }
                );
            modelBuilder.Entity<Tootajad>().HasData(
                new Tootajad { TootajadId = 1, Nimi = "Anton", Vanus = 30, Staaz = 10, Email="anton.velo@gmail.com", Telefon="+372 7898 4654"},
                new Tootajad { TootajadId = 2, Nimi = "Stas", Vanus = 45, Staaz = 15, Email = "stas.velo@gmail.com", Telefon = "+372 7898 4675"},
                new Tootajad { TootajadId = 3, Nimi = "Julia", Vanus = 22, Staaz = 2, Email = "julia.velo@gmail.com", Telefon = "+372 7898 4682"}
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
