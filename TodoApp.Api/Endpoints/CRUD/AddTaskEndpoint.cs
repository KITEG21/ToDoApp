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

[Authorize]
public class AddTaskEndpoint : Endpoint<TaskDto, object>
{
    private readonly ITaskServices _taskServices;
    public AddTaskEndpoint(ITaskServices taskServices)
    {
        _taskServices = taskServices;
    }

    public override void Configure(){
        Post("/api/addTask");
        AllowAnonymous();
    }

    public override async Task HandleAsync(TaskDto req, CancellationToken ct)
    {
        TaskValidator validator = new();
        ValidationResult validationResult = validator.Validate(req);

        if(!validationResult.IsValid){
            var ErrorResponse = new{
                Error = 400,
                Message = validationResult.Errors
            };
            await SendAsync(ErrorResponse, 400, ct);
            return;
        }
        // var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        // if(userIdClaim == null){
        //     await SendUnauthorizedAsync(ct);
        //     return;
        // }

        // int userId = int.Parse(userIdClaim.Value);
        // req.UserId = userId;

        TaskModel newTask = await _taskServices.CreateTask(req);
        newTask.TaskState = ETaskState.Pending;
        await SendAsync(newTask);
        
    }


}
