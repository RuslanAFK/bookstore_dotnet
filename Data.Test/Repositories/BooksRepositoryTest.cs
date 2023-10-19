namespace Data.Test.Repositories;

public class BooksRepositoryTest
{
    private BooksRepository booksRepository = null!;
    private AppDbContext dbContext = null!;
    [SetUp]
    public void Setup()
    {
        var options = DataGenerator.CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        booksRepository = new BooksRepository(dbContext);
    }
    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
    [Test]
    public async Task GetIncludingBookFilesAsync_WithCorrectId_ReturnsBookWithNoNullBookFile()
    {
        var id = 99;
        var newBook = DataGenerator.CreateTestBook(id);
        await dbContext.AddAsync(newBook);
        await dbContext.SaveChangesAsync();
        var results = await booksRepository.GetIncludingBookFilesAsync(id);
        Assert.That(results.BookFile, Is.Not.Null);
    }
    [Test]
    public void GetIncludingBookFilesAsync_WithWrongId_ThrowsEntityNotFoundException()
    {
        var id = 99;
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await booksRepository.GetIncludingBookFilesAsync(id);
        });
    }
}