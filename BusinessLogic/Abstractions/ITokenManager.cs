using Domain.Models;

namespace Services.Abstractions;

public interface ITokenManager
{
    string GenerateToken(string username, string roleName);
}