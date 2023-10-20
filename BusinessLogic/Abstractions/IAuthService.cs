using Domain.Models;
using Services.Dtos;

namespace Services.Abstractions;

public interface IAuthService
{
    Task RegisterUserAsync(User userToCreate);
    Task<AuthResult> GetAuthCredentialsAsync(User user);
    Task UpdateProfileAsync(string username, User newUser, string? newPassword);
    Task DeleteAccountAsync(string username, string inputtedPassword);
    Task AddDefaultCreator();
}