using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Domain.DTOs;
using TodoApp.Domain.Models;

namespace TodoApp.Application.Interfaces;

public interface ITaskServices
{
  public Task<TaskModel> CreateTask(TaskDto req);
  public Task<List<TaskModel>?> GetAllTasks();
  public Task<TaskModel?> GetTask(int id);
  public Task<TaskModel?> UpdateTaskContent(int id, string content); 
  public Task<TaskModel?> UpdateTaskState(int id, ETaskState status);
  public Task<bool> DeleteTask(int id);



}
