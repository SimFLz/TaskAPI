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

    public readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
       var result = await _authService.RegisterAsync(request);
        if (result == null)
        {
            return BadRequest(new
            {
                message = "Email já cadastrado."
            });
        }
        return Ok(result);

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        if (result == null)
        {
            return Unauthorized(new
            {
                message = "Email ou Senha Inválidos."
            });
        }
        return Ok(result);
    }


}
