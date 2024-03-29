﻿namespace BookStoreServer.Test.Controllers;

public class UsersControllerTest
{
    private UsersController usersController = null!;
    [SetUp]
    public void Setup()
    {
        var usersService = A.Fake<IUsersService>();
        usersController = new UsersController(usersService);
    }

    [TearDown]
    public void Teardown()
    {
        usersController.Dispose();
    }
    [Test]
    public async Task GetQueried_ReturnValueIsListResponseResourceOfGetUsersResource()
    {
        var query = A.Dummy<Query>();
        var results = await usersController.GetQueried(query) as OkObjectResult;
        var resultsValue = results?.Value;
        Assert.That(resultsValue, Is.InstanceOf<ListResponse<GetUsersDto>>());
    }
    [Test]
    public async Task GetById_ReturnValueIdGetUserResource()
    {
        var id = A.Dummy<int>();
        var results = await usersController.GetById(id) as OkObjectResult;
        var resultsValue = results?.Value;
        Assert.That(resultsValue, Is.InstanceOf<GetUsersDto>());
    }
    [Test]
    public async Task Update_ReturnsNoContentResult()
    {
        var resource = A.Dummy<UserRoleDto>();
        var id = A.Dummy<int>();
        var results = await usersController.UpdateRole(id, resource);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task Delete_ReturnsNoContentResult()
    {
        var id = A.Dummy<int>();
        var results = await usersController.Delete(id);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }
}