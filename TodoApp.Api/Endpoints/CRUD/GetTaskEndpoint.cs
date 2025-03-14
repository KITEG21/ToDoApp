using System;
using System.Collections.Generic;
using System.Linq;
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
        AllowAnonymous();
    }

    public override async Task HandleAsync(TaskModel _task, CancellationToken ct)
    {
        var id = Route<int>("id");
        var task = await _taskServices.GetTask(id);

        if(task == null)
        {
            var ErrorResponse = new{
                Error = 404,
                Message = "The requested task doens't exist"
            };

           await SendAsync(ErrorResponse);
            return;
        }

        await SendAsync(task);
        return;
    
    }
}
