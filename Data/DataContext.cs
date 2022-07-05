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

    public DbSet<Character> Characters { get; set; }

    public DbSet<User> Users { get; set; }
  }
}