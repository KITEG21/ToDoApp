using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastEndpoints;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.DTOs;

namespace TodoApp.Api.Endpoints.CRUD;

public class DeleteTaskEndpoint: Endpoint<DeleteTaskRequest>
{
private readonly ITaskServices _taskServices;

  public DeleteTaskEndpoint(ITaskServices taskServices)
  {
    _taskServices = taskServices;
  }

    public override void Configure()
    {
        Delete("api/deleteTask/{id}");
        Roles("User");
    }

    public override async Task HandleAsync(DeleteTaskRequest req, CancellationToken ct)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if(userIdClaim == null){
            await SendUnauthorizedAsync(ct);
            return;
        }

        int userId = int.Parse(userIdClaim.Value);
        var id = Route<int>("id");
        bool success = await _taskServices.DeleteTask(id, userId);
        
        if(!success){
            var ErrorResponse = new{
                Code = 404,
                Error = "The requested task doesn't exist"
            };
            await SendAsync(ErrorResponse, cancellation: ct);
            return;
        }

        await SendAsync("The task: "+ id +" has been sucesfully deleted");
        return;
    }

}
