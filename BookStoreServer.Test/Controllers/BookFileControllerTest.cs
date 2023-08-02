using BookStoreServer.Controllers;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Test.Controllers;

public class BookFileControllerTest
{
    private IBooksService booksService;
    private IBookFilesService bookFilesService;
    private BookFileController bookFileController;
    [SetUp]
    public void Setup()
    {
        booksService = A.Fake<IBooksService>();
        bookFilesService = A.Fake<IBookFilesService>();
        bookFileController = new BookFileController(booksService, bookFilesService);
    }

    [Test]
    public async Task Create_ReturnsNoContent()
    {
        var formFile = A.Fake<IFormFile>();
        var returns = await bookFileController.Create(default, formFile);
        Assert.That(returns, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Create_CallsThrowIfNotPermittedAndGetByIdAsyncAndAddAsync()
    {
        var formFile = A.Fake<IFormFile>();
        await bookFileController.Create(default, formFile);
        A.CallTo(() => bookFilesService.ThrowIfFileNotPermitted(A<IFormFile>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => booksService.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => bookFilesService.AddAsync(A<Book>._, A<IFormFile>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task Delete_ReturnsNoContent()
    {
        var returns = await bookFileController.Delete(default);
        Assert.That(returns, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task Delete_CallsThrowIfNotPermittedAndGetByIdAsyncAndAddAsync()
    {
        await bookFileController.Delete(default);
        A.CallTo(() => booksService.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => bookFilesService.RemoveAsync(A<Book>._)).MustHaveHappenedOnceExactly();
    }
}