using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRPG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // data-seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
              new Skill() { Id = 1, Damage = 30, Name = "Frenzy" },
              new Skill() { Id = 2, Damage = 20, Name = "Fire" },
              new Skill() { Id = 3, Damage = 50, Name = "Blizzard" }
              );
        }

        public DbSet<Character> Characters { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Skill> Skills { get; set; }
    }
}