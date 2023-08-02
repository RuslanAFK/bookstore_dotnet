using AutoMapper;
using BookStoreServer.Controllers;
using BookStoreServer.Controllers.Resources.Books;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Test.Controllers;

public class BooksControllerTest
{
    private IMapper mapper;
    private IBooksService booksService;
    private BooksController booksController;

    [SetUp]
    public void Setup()
    {
        mapper = A.Fake<IMapper>();
        booksService = A.Fake<IBooksService>();
        booksController = new BooksController(mapper, booksService);
    }

    [Test]
    public async Task All_ReturnsOkObjectResult()
    {
        var query = A.Dummy<Query>();
        var results = await booksController.All(query);
        Assert.That(results, Is.InstanceOf<OkObjectResult>());
    }
    [Test]
    public async Task All_ReturnsListResponseResourceOfGetBooksResource()
    {
        var query = A.Dummy<Query>();
        var results = await booksController.All(query) as OkObjectResult;
        Assert.That(results.Value, Is.InstanceOf<ListResponseResource<GetBooksResource>>());
    }

    [Test]
    public async Task All_CallsGetBooksAsync()
    {
        var query = A.Dummy<Query>();
        await booksController.All(query);
        A.CallTo(() => booksService.GetBooksAsync(A<Query>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Get_ReturnsOkObjectResult()
    {
        var results = await booksController.Get(default);
        Assert.That(results, Is.InstanceOf<OkObjectResult>());
    }
    [Test]
    public async Task All_ReturnsGetSingleBookResource()
    {
        var results = await booksController.Get(default) as OkObjectResult;
        Assert.That(results.Value, Is.InstanceOf<GetSingleBookResource>());
    }

    [Test]
    public async Task All_CallsGetByIdAsync()
    {
        await booksController.Get(default);
        A.CallTo(() => booksService.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Create_ReturnsNoContentResult()
    {
        var resource = A.Dummy<CreateBookResource>();
        var results = await booksController.Create(resource);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Create_CallsAddAsync()
    {
        var resource = A.Dummy<CreateBookResource>();
        await booksController.Create(resource);
        A.CallTo(() => booksService.AddAsync(A<Book>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Update_ReturnsNoContentResult()
    {
        var resource = A.Dummy<CreateBookResource>();
        var results = await booksController.Update(default, resource);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Update_CallsUpdateAsync()
    {
        var resource = A.Dummy<CreateBookResource>();
        await booksController.Update(default, resource);
        A.CallTo(() => booksService.UpdateAsync(A<int>._, A<Book>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task Delete_ReturnsNoContentResult()
    {
        var results = await booksController.Delete(default);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Create_CallsRemoveAsyncAndGetByIdAsync()
    {
        await booksController.Delete(default);
        A.CallTo(() => booksService.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => booksService.RemoveAsync(A<Book>._)).MustHaveHappenedOnceExactly();
    }
}