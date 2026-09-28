using TaskAPI.Entities;

namespace TaskAPI.Services;

public interface ITokenService
{
    string GenerateToken(UserEntitie user);
}
