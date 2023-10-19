namespace BookStoreServer.Test.Controllers;

public class AuthControllerTest
{
    private AuthController authController = null!;
    [SetUp]
    public void Setup()
    {
        var usersService = A.Fake<IUsersService>();
        var authService = A.Fake<IAuthService>();
        authController = new AuthController(usersService, authService);
    }

    [TearDown]
    public void Teardown()
    {
        authController.Dispose();
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