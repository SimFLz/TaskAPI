using TaskAPI.Entities;
using TaskAPI.Requests;
using TaskAPI.Responses;

namespace TaskAPI.Services;

public interface IAuthService
{
    Task<UserEntitie?> RegisterAsync(RegisterRequest request);
    Task<LoginResponse?> LoginAsync(LoginRequest request);

}
