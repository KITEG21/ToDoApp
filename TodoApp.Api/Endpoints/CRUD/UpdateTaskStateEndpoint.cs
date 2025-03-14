using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastEndpoints;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.DTOs;
using TodoApp.Domain.Validators;

namespace TodoApp.Api.Endpoints.CRUD;

public class UpdateTaskStateEndpoint: Endpoint<TaskStateDto>
{
    private readonly ITaskServices _taskServices;
    public UpdateTaskStateEndpoint(ITaskServices taskServices)
    {
        _taskServices = taskServices;
    }
    public override void Configure()
    {
        Put("api/updateState/{id}");
        Roles("User");
    }
    public override async Task HandleAsync(TaskStateDto req, CancellationToken ct)
    {
        TaskStateValidator validations = new();
        var validationResult = validations.Validate(req);
        if(!validationResult.IsValid){
            var ErrorResponse = new{
                Error = 400,
                Message = validationResult.Errors
            };
            await SendAsync(ErrorResponse, 400, ct);
            return;
        }
        
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if(userIdClaim == null){
            await SendUnauthorizedAsync(ct);
            return;
        }

        int userId = int.Parse(userIdClaim.Value);
        var id = Route<int>("id");
        var task = await _taskServices.UpdateTaskState(id, req.TaskState, userId);
        if(task == null)
        {
            var ErrorResponse = new{
                Error = 404,
                Message = "The requested task doesn't exist"
            };
            await SendAsync(ErrorResponse, 404, ct);
            return;
        }

        await SendAsync(task);
        return;
    }

}