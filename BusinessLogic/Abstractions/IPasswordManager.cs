using Domain.Models;

namespace Services;

public interface IPasswordManager
{
    void SecureUser(User user);
    public void SecureUserWithNewPassword(User user, string newPassword);
    void ThrowExceptionIfWrongPassword(string realPassword, string hashedPassword);
}