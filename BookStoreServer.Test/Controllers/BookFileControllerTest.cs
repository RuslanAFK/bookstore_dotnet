namespace BookStoreServer.Test.Controllers;

public class BookFileControllerTest
{
    private BookFileController bookFileController = null!;
    [SetUp]
    public void Setup()
    {
        var booksService = A.Fake<IBooksService>();
        var bookFilesService = A.Fake<IBookFilesService>();
        bookFileController = new BookFileController(bookFilesService);
    }

    [TearDown]
    public void Teardown()
    {
        bookFileController.Dispose();
    }
    [Test]
    public async Task Create_ReturnsNoContent()
    {
        var formFile = A.Fake<IFormFile>();
        var id = A.Dummy<int>();
        var results = await bookFileController.Create(id, formFile);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task Delete_ReturnsNoContent()
    {
        var id = A.Dummy<int>();
        var results = await bookFileController.Delete(id);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }
}