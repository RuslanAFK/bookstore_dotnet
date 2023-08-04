namespace BookStoreServer.Test.Controllers;

public class AuthControllerTest
{
    private AuthController authController;
    [SetUp]
    public void Setup()
    {
        var mapper = A.Fake<IMapper>();
        var usersService = A.Fake<IUsersService>();
        var authService = A.Fake<IAuthService>();
        authController = new AuthController(mapper, usersService, authService);
    }
    [Test]
    public async Task Login_ReturnValueIsAuthResultResource()
    {
        var loginResource = A.Dummy<LoginResource>();
        var results = await authController.Login(loginResource) as OkObjectResult;
        var resultsValue = results?.Value;
        Assert.That(resultsValue, Is.InstanceOf<AuthResultResource>());
    }
    [Test]
    public async Task Register_ReturnsNoContent()
    {
        var resource = A.Dummy<RegisterResource>();
        var result = await authController.Register(resource);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task UpdateProfile_ReturnsNoContent()
    {
        var resource = A.Dummy<UpdateUserInfoResource>();
        var result = await authController.UpdateProfile(resource);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task DeleteAccount_ReturnsNoContentResult()
    {
        var resource = A.Dummy<DeleteUserResource>();
        var result = await authController.DeleteAccount(resource);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}