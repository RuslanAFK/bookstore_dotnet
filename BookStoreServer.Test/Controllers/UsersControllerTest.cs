using AutoMapper;
using BookStoreServer.Controllers;
using BookStoreServer.Controllers.Resources.Books;
using BookStoreServer.Controllers.Resources.Users;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Test.Controllers;

public class UsersControllerTest
{
    private IMapper mapper;
    private IUsersService usersService;
    private UsersController usersController;

    [SetUp]
    public void Setup()
    {
        mapper = A.Fake<IMapper>();
        usersService = A.Fake<IUsersService>();
        usersController = new UsersController(mapper, usersService);
    }
    [Test]
    public async Task All_ReturnsOkObjectResult()
    {
        var query = A.Dummy<Query>();
        var results = await usersController.All(query);
        Assert.That(results, Is.InstanceOf<OkObjectResult>());
    }
    [Test]
    public async Task All_ReturnsListResponseResourceOfGetUsersResource()
    {
        var query = A.Dummy<Query>();
        var results = await usersController.All(query) as OkObjectResult;
        Assert.That(results.Value, Is.InstanceOf<ListResponseResource<GetUsersResource>>());
    }

    [Test]
    public async Task All_CallsGetQueriedAsync()
    {
        var query = A.Dummy<Query>();
        await usersController.All(query);
        A.CallTo(() => usersService.GetQueriedAsync(A<Query>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Get_ReturnsOkObjectResult()
    {
        var results = await usersController.Get(default);
        Assert.That(results, Is.InstanceOf<OkObjectResult>());
    }
    [Test]
    public async Task All_ReturnsGetUserResource()
    {
        var results = await usersController.Get(default) as OkObjectResult;
        Assert.That(results.Value, Is.InstanceOf<GetUsersResource>());
    }

    [Test]
    public async Task All_CallsGetByIdAsync()
    {
        await usersController.Get(default);
        A.CallTo(() => usersService.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Update_ReturnsNoContentResult()
    {
        var resource = A.Dummy<UserRoleResource>();
        var results = await usersController.UpdateRole(default, resource);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Update_CallsUpdateAsyncAndGetByIdAsync()
    {
        var resource = A.Dummy<UserRoleResource>();
        await usersController.UpdateRole(default, resource);
        A.CallTo(() => usersService.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => usersService.AddUserToRoleAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Delete_ReturnsNoContentResult()
    {
        var results = await usersController.Delete(default);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Create_CallsRemoveAsyncAndGetByIdAsync()
    {
        await usersController.Delete(default);
        A.CallTo(() => usersService.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => usersService.RemoveAsync(A<User>._)).MustHaveHappenedOnceExactly();
    }
}