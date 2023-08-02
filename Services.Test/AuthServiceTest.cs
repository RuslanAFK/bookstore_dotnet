using System.Security.Claims;
using System.Security.Principal;
using Domain.Constants;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Services.Test;

public class AuthServiceTest
{
    private AuthService authService;
    private User realUser;
    private User fakeUser;

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

        realUser = new()
        {
            Id = 0, Name = "Dummy", RoleId = 1, Password = "password"
        };
        fakeUser = A.Fake<User>();

        authService = new AuthService(usersRepository, rolesRepository, tokenManager, unitOfWork, passwordManager);
    }
    [Test]
    public void RegisterAsync_WithFakeUser_CalledAllMethodsOnce()
    {
        async Task Action() => await authService.RegisterAsync(fakeUser);
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => passwordManager.SecureUser(A<User>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => usersRepository.AddAsync(A<User>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => rolesRepository.AssignToRoleAsync(A<User>._, Roles.User)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task GetAuthCredentials_WithAnyUser_ReturnsAuthResult()
    {
        var credentials = await authService.GetAuthCredentialsAsync(fakeUser);
        Assert.IsInstanceOf<AuthResult>(credentials);
    }
    [Test]
    public async Task GetAuthCredentials_WithRealUser_CalledEveryMethodOnce()
    {
        await authService.GetAuthCredentialsAsync(realUser);
        A.CallTo(() => usersRepository.GetByNameAsync(realUser.Name)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.ThrowExceptionIfWrongPassword(realUser.Password, null)).MustHaveHappenedOnceExactly();
        A.CallTo(() => rolesRepository.GetRoleNameByIdAsync(0)).MustHaveHappenedOnceExactly();
        A.CallTo(() => tokenManager.GenerateToken(A<User>._, string.Empty)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void UpdateProfileAsync_WithNewPassword_CalledCheckPasswordAndNotSecureUser()
    {
        async Task Action() => await authService.UpdateUsernameAsync(realUser, fakeUser, "password2");
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => passwordManager.ThrowExceptionIfWrongPassword(null, realUser.Password)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.SecureUserWithNewPassword(realUser, "password2")).MustHaveHappenedOnceExactly();
    }
    [Test]
    public void UpdateProfileAsync_WithoutNewPassword_CalledNeededMethods()
    {
        async Task Action() => await authService.UpdateUsernameAsync(realUser, fakeUser, null);
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => passwordManager.ThrowExceptionIfWrongPassword(null, realUser.Password)).MustHaveHappenedOnceExactly();
        A.CallTo(() => passwordManager.SecureUserWithNewPassword(realUser, "password2")).MustNotHaveHappened();
    }
    [Test]
    public void UpdateProfileAsync_WithRealUsers_UsernameChanged()
    {
        var givenUser = new User
        {
            Name = "Stephen", Password = "password3"
        };
        var userInDb = new User
        {
            Name = "Ivan",
            Password = "password"
        };
        async Task Action() => await authService.UpdateUsernameAsync(userInDb, givenUser, "password2");
        Assert.DoesNotThrowAsync(Action);
        Assert.That(userInDb.Name, Is.EqualTo(givenUser.Name));
    }

    [Test]
    public void DeleteAccountAsync_WithRealUser_CallsCheckPasswordAndRemoveOnce()
    {
        var userInDb = new User
        {
            Name = "Ivan",
            Password = "password"
        };
        async Task Action() => await authService.DeleteAccountAsync(userInDb,  "password2");
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => passwordManager.ThrowExceptionIfWrongPassword("password2", userInDb.Password)).MustHaveHappenedOnceExactly();
        A.CallTo(() => usersRepository.Remove(userInDb)).MustHaveHappenedOnceExactly();
    }


    [Test]
    public void GetUsernameOrThrow_WithFakeIdentity_ThrowsUserNotAuthorizedException()
    {
        var claimsPrincipal = A.Fake<ClaimsPrincipal>();
        void Action() => authService.GetUsernameOrThrow(claimsPrincipal);
        Assert.Throws<UserNotAuthorizedException>(Action);
    }
    [Test]
    public void GetUsernameOrThrow_WithoutNeededClaimAndCtor_ThrowsUserNotFoundException()
    {
        var claims = new List<Claim>();
        IIdentity identity = new ClaimsIdentity(claims, "Bearer");
        var principal = new ClaimsPrincipal(identity);
        void Action() => authService.GetUsernameOrThrow(principal);
        Assert.Throws<EntityNotFoundException>(Action);
    }
    [Test]
    public void GetUsernameOrThrow_WithNeededClaimAndCtor_ReturnsUsername()
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.UniqueName, "Stephen"),
            new(ClaimTypes.Role, "User")
        };
        IIdentity identity = new ClaimsIdentity(claims, "Bearer", 
            JwtRegisteredClaimNames.UniqueName, ClaimTypes.Role);
        var principal = new ClaimsPrincipal(identity);
        var username = authService.GetUsernameOrThrow(principal);
        Assert.That(username == "Stephen");
    }

}