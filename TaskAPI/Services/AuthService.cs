using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using TaskAPI.Data;
using TaskAPI.Requests;

using TaskAPI.Responses;

namespace TaskAPI.Services;

public class AuthService : IAuthService
{

    private readonly AppDbContext _context;
    
    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync<TaskAPI.Entities.UserEntitie>(u => u.Email == request.Email);

        if (user == null) return null;
        if(user.PasswordHash != request.Password) return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),

        };
        var minhaChaveSecreta = "EssaEhUmaFraseSuperSecretaELongaDeTrintaEDoisCaracteres!";
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(minhaChaveSecreta));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(claims: claims,expires: DateTime.UtcNow.AddHours(2),signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new LoginResponse
        {
            Token = tokenString
        };
    }
}
