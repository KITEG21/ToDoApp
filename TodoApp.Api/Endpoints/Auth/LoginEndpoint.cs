using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Models.Auth;
using TodoApp.Infrastructure.Database;

namespace TodoApp.Api.Endpoints.Auth;

public class LoginEndpoint : Endpoint<LoginRequest>
{
   private readonly AppDbContext _context;
   private readonly ITokenServices _tokenServices;
   public LoginEndpoint(AppDbContext context, ITokenServices tokenServices)
   {
        _context = context;
        _tokenServices = tokenServices;
   }

    public override void Configure()
    {
      Post("/api/auth/login");
      AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);
        
        if (user == null)
        {
          var ErrorResponse = new
          {
            Code = 401, 
            Error = "Invalid credentials"
          };  
          await SendAsync(ErrorResponse);
          return;
        }

        var token = _tokenServices.GenerateToken(user.Username, user.Id, "User");
        await SendAsync(new LoginResponse{Token = token}, cancellation:ct);
        return;
    
    }

}
