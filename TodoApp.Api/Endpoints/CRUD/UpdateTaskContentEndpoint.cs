using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        Roles("User");
    }

    public override async Task HandleAsync(TaskContentDto req, CancellationToken ct)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if(userIdClaim == null){
          await SendUnauthorizedAsync(ct);
          return;
        }
        int userId = int.Parse(userIdClaim.Value);

        var id = Route<int>("id");
        var task = await _taskServices.UpdateTaskContent(id, req.Content, userId);
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
