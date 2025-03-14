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
  public Task<List<TaskModel>?> GetAllTasks(int userId);
  public Task<TaskModel?> GetTask(int id, int userId);
  public Task<TaskModel?> UpdateTaskContent(int id, string content, int userId); 
  public Task<TaskModel?> UpdateTaskState(int id, ETaskState status, int userId);
  public Task<bool> DeleteTask(int id, int userId);



}
