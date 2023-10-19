using System.Security.Claims;
using Domain.Models;

namespace Services.Abstractions;

public interface IAuthService
{
    Task RegisterAsync(User userToCreate);
    Task<AuthResult> GetAuthCredentialsAsync(User user);
    Task UpdateProfileAsync(User existingUser, User newUser, string? newPassword);
    Task DeleteAccountAsync(User user, string inputtedPassword);
    string GetUsernameOrThrow(ClaimsPrincipal? claimsPrincipal);
}