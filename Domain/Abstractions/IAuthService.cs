using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IAuthService
    {
        Task RegisterAsync(User userToCreate);
        Task<AuthResult> GetAuthCredentials(User user);
        Task UpdateProfileAsync(User foundUser, User user, string? newPassword);
        Task DeleteAccountAsync(User user, string inputtedPassword);
        string GetUsernameOrThrow(IIdentity? identity);
    }
}
