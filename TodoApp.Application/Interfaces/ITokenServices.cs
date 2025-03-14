using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Application.Interfaces;

public interface ITokenServices
{
    string GenerateToken(string username, int userId,string role);
}
