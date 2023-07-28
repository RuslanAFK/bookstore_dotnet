using Domain.Models;

namespace Domain.Abstractions;

public interface ITokenManager
{
    string GenerateToken(User user, string roleName);
}