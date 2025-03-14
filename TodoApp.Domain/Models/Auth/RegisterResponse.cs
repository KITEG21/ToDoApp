using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Domain.Models.Auth
{
    public class RegisterResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}