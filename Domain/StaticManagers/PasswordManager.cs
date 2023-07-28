using Domain.Exceptions;
using Domain.Models;

namespace Domain.StaticManagers
{
    public static class PasswordManager
    {
        public static User GetSecuredUser(User user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            return user;
        }
        public static void CheckPassword(string realPassword, string hashedPassword)
        {
            if (!BCrypt.Net.BCrypt.Verify(realPassword, hashedPassword))
                throw new WrongPasswordException();
        }
    }
}
