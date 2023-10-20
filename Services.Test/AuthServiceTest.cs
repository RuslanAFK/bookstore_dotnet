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
        var newPassword = A.Dummy<string>();
        await authService.UpdateProfileAsync(A.Dummy<string>(), newUser, newPassword);
        A.CallTo(() => passwordManager.CheckPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.SecureUserWithNewPassword(A<User>._, A< string >._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task UpdateProfileAsync_WithoutNewPassword_CalledCheckPasswordAndNotSecureUser()
    {
        var newUser = A.Dummy<User>();
        await authService.UpdateProfileAsync(A.Dummy<string>(), newUser, null);
        A.CallTo(() => passwordManager.CheckPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.SecureUserWithNewPassword(A<User>._, A<string>._)).MustNotHaveHappened();
    }
    
    [Test]
    public void DeleteAccountAsync_CallsThrowExceptionIfWrongPasswordAndRemove()
    {
        var dummyPassword = A.Dummy<string>();
        async Task Action() => await authService.DeleteAccountAsync(A.Dummy<string>(), dummyPassword);
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => passwordManager.CheckPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
    }
}