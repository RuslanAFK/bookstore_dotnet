using Domain.Exceptions;
using Domain.Models;

namespace Services;

public class PasswordManager : IPasswordManager
{
    public void SecureUser(User user)
    {
        SecureUser(user, user.Password);
    }
    public void SecureUserWithNewPassword(User user, string newPassword)
    {
        SecureUser(user, newPassword);
    }

    private void SecureUser(User user, string passwordToHash)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordToHash);
        user.Password = hashedPassword;
    }
    public void ThrowExceptionIfWrongPassword(string realPassword, string hashedPassword)
    {
        if (!BCrypt.Net.BCrypt.Verify(realPassword, hashedPassword))
            throw new WrongPasswordException();
    }
}