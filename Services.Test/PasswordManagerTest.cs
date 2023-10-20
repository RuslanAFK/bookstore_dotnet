namespace Services.Test;

public class PasswordManagerTest
{
    private PasswordManager passwordManager = null!;
    [SetUp]
    public void Setup()
    {
        passwordManager = new PasswordManager();
    }
    [Test]
    public void SecureUser_VerifyWithBCrypt()
    {
        var textPassword = "password";
        var user = DataGenerator.CreateTestUser(password: textPassword);
        user.Password = passwordManager.SecureUser(user);
        var isHashingCorrect = BCrypt.Net.BCrypt.Verify(textPassword, user.Password);
        Assert.That(isHashingCorrect, Is.True);
    }
    [Test]
    public void SecureUserWithNewPassword_VerifyWithBCrypt()
    {
        var oldPassword = "oldPassword";
        var newPassword = "password";
        var user = DataGenerator.CreateTestUser(password: oldPassword);
        user.Password = passwordManager.SecureUserWithNewPassword(user, newPassword);
        var isHashingCorrect = BCrypt.Net.BCrypt.Verify(newPassword, user.Password);
        Assert.That(isHashingCorrect, Is.True);
    }

    [Test]
    public void ThrowExceptionIfWrongPassword_WithCorrectPassword_Passes()
    {
        var password = "password";
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        passwordManager.CheckPassword(password, hashed);
        Assert.Pass();
    }
    [Test]
    public void ThrowExceptionIfWrongPassword_WithWrongPassword_ThrowsWrongPasswordException()
    {
        var password = "password";
        var wrongPassword = "wrong";
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        Assert.Throws<WrongPasswordException>(() => 
            passwordManager.CheckPassword(wrongPassword, hashed));
    }
    [Test]
    public void ThrowExceptionIfWrongPassword_WithNotHashedValue_PasswordNotParseableException()
    {
        var password = "password";
        var notHashedPassword = "not hashed at all";
        Assert.Throws<PasswordNotParseableException>(() =>
        {
            passwordManager.CheckPassword(password, notHashedPassword);
        });
    }
}