using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Models;
using TodoApp.Domain.Models.Auth;
using TodoApp.Infrastructure.Database;

namespace TodoApp.Api.Endpoints.Auth;

public class SignUpEndpoint : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly AppDbContext _context;
    private readonly ITokenServices _tokenServices;
    public SignUpEndpoint(AppDbContext context, ITokenServices tokenServices)
    {
        _context = context;
        _tokenServices = tokenServices;
    }

    public override void Configure()
    {
        Post("api/auth/signup");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        if(await _context.Users.AnyAsync(x => x.Username == req.Username || x.Email == req.Email))
        {
            var errorResponse = new
            {
                StatusCode = 409,
                Errors = "User already exist"
            };
            //await SendAsync(errorResponse, 409, ct);
            return;
        }

        var user = new UserModel
        {
            Username = req.Username,
            Email = req.Email,
            Password = req.Password
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var token = _tokenServices.GenerateToken(user.Username, user.Id, "User");
        await SendAsync(new RegisterResponse{Token = token}, cancellation:ct);
    }
}
