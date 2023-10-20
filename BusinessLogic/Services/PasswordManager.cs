using BCrypt.Net;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;

namespace Services.Services;

public class PasswordManager : IPasswordManager
{
    public string SecureUser(User user)
    {
        return SecureUser(user, user.Password);
    }
    public string SecureUserWithNewPassword(User user, string newPassword)
    {
        return SecureUser(user, newPassword);
    }

    private string SecureUser(User user, string passwordToHash)
    {
        return BCrypt.Net.BCrypt.HashPassword(passwordToHash);
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