using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastEndpoints;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Models;

namespace TodoApp.Api.Endpoints.CRUD;

public class GetTaskEndpoint : Endpoint<TaskModel>
{
    private readonly ITaskServices _taskServices;       

    public GetTaskEndpoint(ITaskServices taskServices)
    {
        _taskServices = taskServices;
    }

    public override void Configure()
    {
        Get("api/task/{id}");
        Roles("User");
    }

    public override async Task HandleAsync(TaskModel _task, CancellationToken ct)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if(userIdClaim == null){
            await SendUnauthorizedAsync(ct);
            return;
        }
        int userId = int.Parse(userIdClaim.Value);

        var id = Route<int>("id");
        var task = await _taskServices.GetTask(id, userId);

        if(task == null)
        {
            var ErrorResponse = new{
                Error = 404,
                Message = "The requested task doesn't exist"
            };

           await SendAsync(ErrorResponse);
           return;
        }

        await SendAsync(task);
        return;
    
    }
}
