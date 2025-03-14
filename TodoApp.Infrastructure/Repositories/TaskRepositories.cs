using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces.Repositories;
using TodoApp.Application.Mapper;
using TodoApp.Domain.DTOs;
using TodoApp.Domain.Models;
using TodoApp.Infrastructure.Database;

namespace TodoApp.Infrastructure.Repositories;

public class TaskRepositories : ITaskRepositories
{
    private readonly AppDbContext _context;
    private readonly TaskMapper _mapper;

    public TaskRepositories(AppDbContext context, TaskMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskModel> CreateTask(TaskDto req){

        TaskModel newTask = _mapper.TaskDtoToTask(req);
        newTask.CreatedDate = DateTime.UtcNow;

        await _context.Tasks.AddAsync(newTask);
        await _context.SaveChangesAsync();
        return newTask;

    }
    public async Task<bool> DeleteTask(int id)
    {
      var taskToDelete = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
      if(taskToDelete == null){
        return false;
      }

      _context.Tasks.Remove(taskToDelete);
      await _context.SaveChangesAsync();
      return true;
    }
    public async Task<List<TaskModel>?> GetAllTasks(){

        var tasks = await _context.Tasks.ToListAsync();
        
        if(tasks == null){
            return null;
        }

        return tasks;
    }
    public async Task<TaskModel?> GetTask(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

        if(task == null){
            return null;
        }

        return task;

    }
    public async Task<TaskModel?> UpdateTaskContent(int id, string content)
    {
        TaskModel? taskToUpdate = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        
        if(taskToUpdate == null){
            return null; 
        } 

        taskToUpdate.Content = content;
        await _context.SaveChangesAsync();
        return taskToUpdate;
        
    }
    public async Task<TaskModel?> UpdateTaskState(int id, ETaskState status)
    {
        TaskModel? taskToUpdate = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

        if(taskToUpdate == null){
            return null;
        }

        taskToUpdate.TaskState = status;
        await _context.SaveChangesAsync();
        return taskToUpdate;
    }
    
}