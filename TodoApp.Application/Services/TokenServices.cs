using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Services;

public class TokenServices : ITokenServices
{

   private readonly IConfiguration _configuration;
   public TokenServices(IConfiguration configuration)
   {
     _configuration = configuration;
   }

    public string GenerateToken(string username, int userId,string role)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySuperDeluxeDuperAmazingCuteUltraSecretKey"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        int expiresInMinutes = 15;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiresInMinutes),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
