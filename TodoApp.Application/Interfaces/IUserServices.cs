using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Domain.Models;
using TodoApp.Domain.Models.Auth;

namespace TodoApp.Application.Interfaces;

public interface IUserServices
{
  public Task<LoginResponse> UserLogin(LoginRequest request);
  public Task<UserModel> UserRegister(RegisterRequest request);

}
