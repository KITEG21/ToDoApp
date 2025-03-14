using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoApp.Domain.Models;

namespace TodoApp.Domain.DTOs;

public class TaskStateDto
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public ETaskState TaskState { get; set; }
        
        
}
