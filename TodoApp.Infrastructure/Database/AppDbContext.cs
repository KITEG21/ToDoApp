using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Models;

namespace TodoApp.Infrastructure.Database;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options)
  { 
  }

  required public DbSet<TaskModel> Tasks { get; set; }
  required public DbSet<UserModel> Users { get; set; }
  
}
