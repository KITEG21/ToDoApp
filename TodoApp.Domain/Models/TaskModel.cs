using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Domain.Models;

public class TaskModel
{   public int Id { get; set; }
    public int UserId { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateOnly DayToDo { get; set; }
    public TimeOnly HourToDo { get; set;}
    public ETaskState TaskState { get; set;}

    
}
