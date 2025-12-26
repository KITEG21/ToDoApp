using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.DTOs;
using TodoApp.Domain.Models;
using TodoApp.Domain.Validators;
using TodoApp.Infrastructure.Database;

namespace TodoApp.Api.Endpoints.CRUD;

public class AddTaskEndpoint : Endpoint<TaskDto, object>
{
    private readonly ITaskServices _taskServices;
    public AddTaskEndpoint(ITaskServices taskServices)
    {
        _taskServices = taskServices;
    }

    public override void Configure(){
        Post("/api/task");
        Roles("User");
        Description(x => x.WithTags("Task"));
    }

    public override async Task HandleAsync(TaskDto req, CancellationToken ct)
    {
        TaskValidator validator = new();
        ValidationResult validationResult = validator.Validate(req);

        if(!validationResult.IsValid){
            await SendErrorsAsync(400, ct);
            return;
        }
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if(userIdClaim == null){
            ThrowError("Unauthorized", 401);
        }

        int userId = int.Parse(userIdClaim.Value);
        req.UserId = userId;

        TaskModel newTask = await _taskServices.CreateTask(req);
        if(newTask == null)
        {
            ThrowError("Failed to create task", 400);
        }
        newTask.TaskState = ETaskState.Pending;
        await SendAsync(newTask);
    }


}
