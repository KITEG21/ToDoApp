using TodoApp.Domain.DTOs;
using TodoApp.Domain.Models;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Interfaces.Repositories;
using Serilog;

namespace TodoApp.Application.Services;

public class TaskServices : ITaskServices
{
    private readonly ITaskRepositories _taskRepositories;
    public TaskServices(ITaskRepositories taskRepositories)
    {
      _taskRepositories = taskRepositories;
    }

    public async Task<TaskModel> CreateTask(TaskDto req)
    {   
        var newTask = await _taskRepositories.CreateTask(req);

        if(newTask == null){
            return null; 
        }
        
        Log.Information("New task created");
        return newTask;
    }

    public async Task<bool> DeleteTask(int id)
    {
        bool success = await _taskRepositories.DeleteTask(id);
        if(!success){
            Log.Information("Failed to delete task");
            return false;
        }

        Log.Information("Task deleted successfully");
        return true;
    }

    public async Task<List<TaskModel>?> GetAllTasks()
    {
        var tasks = await _taskRepositories.GetAllTasks();
        if(tasks == null)
        {
          Log.Information("No task were found");
          return null;
        }
        Log.Information("List of task prepared");
        return tasks;
    }

    public async Task<TaskModel?> GetTask(int id)
    {
       var task = await _taskRepositories.GetTask(id);
        if(task == null)
        {
          Log.Information("Requested task doesn't exists");
          return null;
        }
        Log.Information("Task with ID: {id} found",id);
        return task;
    }

    public async Task<TaskModel?> UpdateTaskContent(int id, string content)
    {
        var task = await _taskRepositories.UpdateTaskContent(id, content);
        if(task == null)
        {
          Log.Information("Requested task doesn't exists");
          return null;
        }
        Log.Information("Task content edited successfully");
        return task;
    }

    public async Task<TaskModel?> UpdateTaskState(int id, ETaskState status)
    {
        var task = await _taskRepositories.UpdateTaskState(id, status);
        if(task == null)
        {
          Log.Information("Requested task doesn't exists");
          return null;
        }
        Log.Information("Task state edited successfully");
        return task;
    }
}
