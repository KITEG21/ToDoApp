using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Domain.Models;

public class UserModel
{
  public string Username { get; set; } = string.Empty;
  public int Id { get; set; }
  public string Password { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public List<TaskModel>? TaskList { get; set;}
  

  
  
}
