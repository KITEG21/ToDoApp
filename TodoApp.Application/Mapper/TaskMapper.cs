using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;
using Riok.Mapperly;
using TodoApp.Domain.DTOs;
using TodoApp.Domain.Models;

namespace TodoApp.Application.Mapper;
  
[Mapper]
public partial class TaskMapper
{
  public partial TaskModel TaskDtoToTask(TaskDto taskDto);
  public partial TaskDto TaskToTaskDto(TaskModel task);
}
