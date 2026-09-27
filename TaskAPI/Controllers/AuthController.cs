using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Data;
using TaskAPI.Entities;
using TaskAPI.Requests;
using TaskAPI.Services;

namespace TaskAPI.Controllers;


[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{

    public readonly List<UserEntitie> _users = new();

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
       var userExists = _users.Any(u => u.Email == request.Email);
        if (userExists)
        {
            return Conflict(new
            {
                message = "Usuário já cadastrado."
            });
        }
        var user = new UserEntitie
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        _users.Add(user);
        return Ok("User registered successfully.");

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = _users.FirstOrDefault(u => u.Email == request.Email);

        if(user == null)
        {
            return Unauthorized(new
            {
                message = "Email ou Senha Inválidos."
            });
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if(!isPasswordValid)
        {
            return Unauthorized(new
            {
                message = "Senha Inválida."
            });
        }
        return Ok("Login realizado com sucesso.");
    }


}
