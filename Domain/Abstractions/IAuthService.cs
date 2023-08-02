using System.Security.Claims;
using Domain.Models;

namespace Domain.Abstractions;

public interface IAuthService
{
    Task RegisterAsync(User userToCreate);
    Task<AuthResult> GetAuthCredentialsAsync(User user);
    Task UpdateUsernameAsync(User userInDb, User givenUser, string? newPassword);
    Task DeleteAccountAsync(User user, string inputtedPassword);
    string GetUsernameOrThrow(ClaimsPrincipal? claimsPrincipal);
}