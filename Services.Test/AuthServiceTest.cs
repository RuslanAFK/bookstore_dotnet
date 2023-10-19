using Services.ResponseDtos;

namespace Services.Test;

public class AuthServiceTest
{
    private AuthService authService = null!;
    private ITokenManager tokenManager = null!;
    private IUnitOfWork unitOfWork = null!;
    private IPasswordManager passwordManager = null!;
    [SetUp]
    public void Setup()
    {
        tokenManager = A.Fake<ITokenManager>();
        unitOfWork = A.Fake<IUnitOfWork>();
        passwordManager = A.Fake<IPasswordManager>();
        authService = new AuthService(tokenManager, unitOfWork, passwordManager);
    }
    [Test]
    public async Task RegisterAsync_WithFakeUser_CalledAllMethodsOnce()
    {
        var user = A.Dummy<User>();
        await authService.RegisterAsync(user);
        A.CallTo(() => passwordManager.SecureUser(A<User>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task UpdateProfileAsync_WithNewPassword_CalledCheckPasswordAndSecureUser()
    {
        var newUser = A.Dummy<User>();
        var existingUser = A.Dummy<User>();
        var newPassword = A.Dummy<string>();
        await authService.UpdateProfileAsync(existingUser, newUser, newPassword);
        A.CallTo(() => passwordManager.CheckPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.SecureUserWithNewPassword(A<User>._, A< string >._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task UpdateProfileAsync_WithoutNewPassword_CalledCheckPasswordAndNotSecureUser()
    {
        var newUser = A.Dummy<User>();
        var existingUser = A.Dummy<User>();
        await authService.UpdateProfileAsync(existingUser, newUser, null);
        A.CallTo(() => passwordManager.CheckPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.SecureUserWithNewPassword(A<User>._, A<string>._)).MustNotHaveHappened();
    }

    [Test]
    public async Task UpdateProfileAsync_ChangesUsername()
    {
        var newUser = DataGenerator.CreateTestUser(name: "Steven");
        var existingUser = A.Dummy<User>();
        var newPassword = A.Dummy<string>();

        await authService.UpdateProfileAsync(existingUser, newUser, newPassword);
        var actualName = existingUser.Name;
        var expectedName = newUser.Name;
        Assert.That(actualName, Is.EqualTo(expectedName));
    }
    [Test]
    public void DeleteAccountAsync_CallsThrowExceptionIfWrongPasswordAndRemove()
    {
        var realUser = A.Dummy<User>();
        var dummyPassword = A.Dummy<string>();
        async Task Action() => await authService.DeleteAccountAsync(realUser, dummyPassword);
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => passwordManager.CheckPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
    }
}