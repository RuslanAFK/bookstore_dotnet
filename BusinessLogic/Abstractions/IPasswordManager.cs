using Domain.Models;

namespace Services.Abstractions;

public interface IPasswordManager
{
    void SecureUser(User user);
    public void SecureUserWithNewPassword(User user, string newPassword);
    void CheckPassword(string realPassword, string hashedPassword);
}