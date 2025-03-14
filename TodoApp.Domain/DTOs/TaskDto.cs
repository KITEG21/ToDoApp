using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Domain.Models;

namespace TodoApp.Domain.DTOs;

public class TaskDto
{
  public string Title { get; set; } = string.Empty;
  public int UserId { get; set; }
  public string Content { get; set; } = string.Empty;
  public DateOnly DayToDo { get; set; }
  public TimeOnly HourToDo { get; set;}
  public ETaskState TaskState { get; set;}
}
