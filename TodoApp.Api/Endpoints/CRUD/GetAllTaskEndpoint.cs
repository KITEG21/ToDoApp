using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Models;
using TodoApp.Infrastructure.Database;

namespace TodoApp.Api.Endpoints.CRUD;

public class GetAllTaskEndpoint : EndpointWithoutRequest<List<TaskModel>>
{
    private readonly AppDbContext _context;
    private readonly ITaskServices _taskServices;

    public GetAllTaskEndpoint(AppDbContext context, ITaskServices taskServices)
    {
        _context = context;
        _taskServices = taskServices;
    }
    
   public override void Configure(){
        Get("/api/getTasks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var task = await _taskServices.GetAllTasks();
        await SendAsync(task);

    }

}
