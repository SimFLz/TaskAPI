using TaskAPI.Requests;
using TaskAPI.Responses;

namespace TaskAPI.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);

}
