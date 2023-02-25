using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface ITokenManager
{
    string GenerateToken(User user, string roleName);
}