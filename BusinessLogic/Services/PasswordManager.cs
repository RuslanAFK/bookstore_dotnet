using BCrypt.Net;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;

namespace Services.Services;

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
    public void CheckPassword(string realPassword, string hashedPassword)
    {
        try
        {
            var passwordCorrect = 
                BCrypt.Net.BCrypt.Verify(realPassword, hashedPassword);
            if (!passwordCorrect)
                throw new WrongPasswordException();
        }
        catch (SaltParseException)
        {
            throw new PasswordNotParseableException();
        }
    }
}