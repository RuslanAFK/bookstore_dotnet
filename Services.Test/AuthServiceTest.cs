namespace Services.Test;

public class AuthServiceTest
{
    private AuthService authService;
    private IUsersRepository usersRepository;
    private ITokenManager tokenManager;
    private IUnitOfWork unitOfWork;
    private IPasswordManager passwordManager;
    private IRolesRepository rolesRepository;
    [SetUp]
    public void Setup()
    {
        rolesRepository = A.Fake<IRolesRepository>();
        tokenManager = A.Fake<ITokenManager>();
        usersRepository = A.Fake<IUsersRepository>();
        unitOfWork = A.Fake<IUnitOfWork>();
        passwordManager = A.Fake<IPasswordManager>();
        authService = new AuthService(usersRepository, rolesRepository, tokenManager, unitOfWork, passwordManager);
    }
    [Test]
    public async Task RegisterAsync_WithFakeUser_CalledAllMethodsOnce()
    {
        var user = A.Dummy<User>();
        await authService.RegisterAsync(user);
        A.CallTo(() => passwordManager.SecureUser(A<User>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => usersRepository.AddAsync(A<User>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => rolesRepository.AssignToRoleAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task GetAuthCredentials_WithAnyUser_ReturnsAuthResult()
    {
        var user = A.Dummy<User>();
        var credentials = await authService.GetAuthCredentialsAsync(user);
        Assert.That(credentials, Is.InstanceOf<AuthResult>());
    }
    [Test]
    public async Task UpdateProfileAsync_WithNewPassword_CalledCheckPasswordAndSecureUser()
    {
        var newUser = A.Dummy<User>();
        var existingUser = A.Dummy<User>();
        var newPassword = A.Dummy<string>();
        await authService.UpdateProfileAsync(existingUser, newUser, newPassword);
        A.CallTo(() => passwordManager.ThrowExceptionIfWrongPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.SecureUserWithNewPassword(A<User>._, A< string >._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task UpdateProfileAsync_WithoutNewPassword_CalledCheckPasswordAndNotSecureUser()
    {
        var newUser = A.Dummy<User>();
        var existingUser = A.Dummy<User>();
        await authService.UpdateProfileAsync(existingUser, newUser, null);
        A.CallTo(() => passwordManager.ThrowExceptionIfWrongPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
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
        A.CallTo(() => passwordManager.ThrowExceptionIfWrongPassword(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => usersRepository.Remove(A<User>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public void GetUsernameOrThrow_WithFakeIdentity_ThrowsUserNotAuthorizedException()
    {
        var claimsPrincipal = A.Fake<ClaimsPrincipal>();
        Assert.Throws<UserNotAuthorizedException>(() =>
        {
            authService.GetUsernameOrThrow(claimsPrincipal);
        });
    }
    [Test]
    public void GetUsernameOrThrow_WithoutNeededClaimAndCtor_ThrowsUserNotFoundException()
    {
        var claims = new List<Claim>();
        var authenticationType = "Bearer";
        var identity = new ClaimsIdentity(claims, authenticationType);
        var principal = new ClaimsPrincipal(identity);
        Assert.Throws<EntityNotFoundException>(() =>
        {
            authService.GetUsernameOrThrow(principal);
        });
    }
    [Test]
    public void GetUsernameOrThrow_WithNeededClaimAndCtor_ReturnsUsername()
    {
        var username = "Stephen";
        var role = "User";
        var claims = new List<Claim>
        {
            new(ClaimNames.UniqueName, username),
            new(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "Bearer", 
            ClaimNames.UniqueName, ClaimTypes.Role);
        var principal = new ClaimsPrincipal(identity);
        var foundUsername = authService.GetUsernameOrThrow(principal);
        Assert.That(foundUsername == username);
    }
}