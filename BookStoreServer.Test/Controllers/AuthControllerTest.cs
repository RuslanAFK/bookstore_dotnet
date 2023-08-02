using System.Security.Claims;
using AutoMapper;
using BookStoreServer.Controllers;
using BookStoreServer.Controllers.Resources.Auth;
using BookStoreServer.Controllers.Resources.Users;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Test.Controllers;

public class AuthControllerTest
{
    private IMapper mapper;
    private IUsersService usersService;
    private IAuthService authService;
    private AuthController authController;
    [SetUp]
    public void Setup()
    {
        mapper = A.Fake<IMapper>();
        usersService = A.Fake<IUsersService>();
        authService = A.Fake<IAuthService>();
        authController = new AuthController(mapper, usersService, authService);
    }

    [Test]
    public async Task Login__ReturnsOkResult()
    {
        var loginResource = A.Dummy<LoginResource>();
        var result = await authController.Login(loginResource);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
    [Test]
    public async Task Login__ReturnsAuthResultResourceValue()
    {
        var loginResource = A.Dummy<LoginResource>();
        var result = await authController.Login(loginResource);
        var okObjectResult = result as OkObjectResult;
        Assert.That(okObjectResult.Value, Is.InstanceOf<AuthResultResource>());
    }
    [Test]
    public async Task Login__CallsGetAuthCredentials()
    {
        var loginResource = A.Dummy<LoginResource>();
        await authController.Login(loginResource);
        A.CallTo(() => authService.GetAuthCredentialsAsync(A<User>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Register__ReturnsNoContentResult()
    {
        var resource = A.Dummy<RegisterResource>();
        var result = await authController.Register(resource);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task Login__Calls()
    {
        var resource = A.Dummy<RegisterResource>();
        await authController.Register(resource);
        A.CallTo(() => authService.RegisterAsync(A<User>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task UpdateProfile__ReturnsNoContentResult()
    {
        var resource = A.Dummy<UpdateUserInfoResource>();
        var result = await authController.UpdateProfile(resource);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task UpdateProfile__CallsGetAuthCredentials()
    {
        var resource = A.Dummy<UpdateUserInfoResource>();
        await authController.UpdateProfile(resource);
        A.CallTo(() => authService.GetUsernameOrThrow(A<ClaimsPrincipal>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => usersService.GetByNameAsync(A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => authService.UpdateUsernameAsync(A<User>._, A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task DeleteAccount__ReturnsNoContentResult()
    {
        var resource = A.Dummy<DeleteUserResource>();
        var result = await authController.DeleteAccount(resource);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task DeleteAccount__CallsGetAuthCredentials()
    {
        var resource = A.Dummy<DeleteUserResource>();
        await authController.DeleteAccount(resource);
        A.CallTo(() => usersService.GetByNameAsync(A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => authService.GetUsernameOrThrow(A<ClaimsPrincipal>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => authService.DeleteAccountAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
    }
}