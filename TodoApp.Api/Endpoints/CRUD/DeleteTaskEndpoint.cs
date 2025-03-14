using System;
using System.Collections.Generic;
using System.Linq;
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
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteTaskRequest req, CancellationToken ct)
    {
        var id = Route<int>("id");
        bool success = await _taskServices.DeleteTask(id);
        
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
