using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using TaskAPI.Data;
using TaskAPI.Entities;
using TaskAPI.Requests;

using TaskAPI.Responses;

namespace TaskAPI.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<UserEntitie?> RegisterAsync(RegisterRequest request)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Email == request.Email);
        if(userExists)
        {
            return null;
        }
        var user = new UserEntitie
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
        {
            Console.WriteLine("USUÁRIO NÃO ENCONTRADO");
            return null;
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash
        );

        Console.WriteLine($"Usuário encontrado: {user.Email}");
        Console.WriteLine($"Senha válida: {passwordValid}");

        if (!passwordValid)
        {
            return null;
        }
        var token = _tokenService.GenerateToken(user);

        return new LoginResponse { Token = token };
    }


}
