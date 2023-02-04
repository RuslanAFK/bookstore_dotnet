using BookStoreServer.Core.Models;

namespace BookStoreServer.Core;

public interface ITokenManager
{
    string GenerateToken(User user, string roleName);
}