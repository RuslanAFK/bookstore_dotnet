using Domain.Models;

namespace Services.Abstractions;

public interface ITokenManager
{
    string GenerateToken(User user, string roleName);
}