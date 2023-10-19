namespace BookStoreServer.Test.Controllers;

public class BooksControllerTest
{
    private BooksController booksController = null!;
    [SetUp]
    public void Setup()
    {
        var booksService = A.Fake<IBooksService>();
        booksController = new BooksController(booksService);
    }

    [TearDown]
    public void Teardown()
    {
        booksController.Dispose();
    }
    [Test]
    public async Task GetQueried_ReturnsListResponseResourceOfGetBooksResource()
    {
        var query = A.Dummy<Query>();
        var results = await booksController.GetQueried(query) as OkObjectResult;
        var resultsValue = results?.Value;
        Assert.That(resultsValue, Is.InstanceOf<ListResponse<GetBooksDto>>());
    }
    [Test]
    public async Task GetById_ReturnsGetSingleBookResource()
    {
        var results = await booksController.GetById(default) as OkObjectResult;
        var resultsValue = results?.Value;
        Assert.That(resultsValue, Is.InstanceOf<GetSingleBookDto>());
    }
    [Test]
    public async Task Create_ReturnsNoContent()
    {
        var resource = A.Dummy<CreateBookResource>();
        var results = await booksController.Create(resource);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task Update_ReturnsNoContent()
    {
        var resource = A.Dummy<CreateBookResource>();
        var id = A.Dummy<int>();
        var results = await booksController.Update(id, resource);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }
    [Test]
    public async Task Delete_ReturnsNoContent()
    {
        var id = A.Dummy<int>();
        var results = await booksController.Delete(id);
        Assert.That(results, Is.InstanceOf<NoContentResult>());
    }
}