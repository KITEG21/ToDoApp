using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.DTOs;
using TodoApp.Domain.Models;

namespace TodoApp.Api.Endpoints.CRUD;

public class UpdateTaskContentEndpoint : Endpoint<TaskContentDto>
{
    private readonly ITaskServices _taskServices;

    public UpdateTaskContentEndpoint(ITaskServices taskServices)
    {
        _taskServices = taskServices;
    }

    public override void Configure()
    {
        Put("api/updateContent/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(TaskContentDto req, CancellationToken ct)
    {
        var id = Route<int>("id");
        var task = await _taskServices.UpdateTaskContent(id, req.Content);
        if(task == null)
        {
            var ErrorResponse = new{
                Error = 404,
                Message = "The requested task doens't exist"
            };
            await SendAsync(ErrorResponse, 404, ct);
            return;
        }

        await SendAsync(task);
        return;
    }




}
