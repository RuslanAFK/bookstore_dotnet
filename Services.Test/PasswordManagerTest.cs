using BCrypt.Net;

namespace Services.Test;

public class PasswordManagerTest
{
    private PasswordManager passwordManager;
    [SetUp]
    public void Setup()
    {
        passwordManager = new PasswordManager();
    }

    [Test]
    public void SecureUser_VerifyWithBCrypt_HashingWorks()
    {
        var textPassword = "password";
        var user = new User { Password = textPassword };

        passwordManager.SecureUser(user);

        var isHashingCorrect = BCrypt.Net.BCrypt.Verify(textPassword, user.Password);
        Assert.IsTrue(isHashingCorrect);
    }
    [Test]
    public void SecureUserWithNewPassword_VerifyWithBCrypt_HashingWorks()
    {
        var newPassword = "password";
        var user = new User { Password = "Whatever" };

        passwordManager.SecureUserWithNewPassword(user, newPassword);

        var isHashingCorrect = BCrypt.Net.BCrypt.Verify(newPassword, user.Password);
        Assert.IsTrue(isHashingCorrect);
    }

    [Test]
    public void ThrowExceptionIfWrongPassword_WithCorrentPassword_Passes()
    {
        var password = "password";
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        passwordManager.ThrowExceptionIfWrongPassword(password, hashed);
        Assert.Pass();
    }
    [Test]
    public void ThrowExceptionIfWrongPassword_WithWrongPassword_ThrowsWrongPasswordException()
    {
        var password = "password";
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        Assert.Throws<WrongPasswordException>(() =>
        {
            passwordManager.ThrowExceptionIfWrongPassword("another", hashed);
        });
    }

    //  Integration tests
    // TODO catch error

    [Test]
    public void ThrowExceptionIfWrongPassword_WithNotHashedValue_ThrowsWrongPasswordException()
    {
        var password = "password";
        Assert.Throws<SaltParseException>(() =>
        {
            passwordManager.ThrowExceptionIfWrongPassword("another", "not hashed");
        });
    }
}