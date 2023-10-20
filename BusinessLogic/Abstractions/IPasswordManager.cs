using Domain.Models;

namespace Services.Abstractions;

public interface IPasswordManager
{
    string SecureUser(User user);
    string SecureUserWithNewPassword(User user, string newPassword);
    void CheckPassword(string realPassword, string hashedPassword);
}