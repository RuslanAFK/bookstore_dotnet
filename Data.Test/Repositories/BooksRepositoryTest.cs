namespace Data.Test.Repositories;

public class BooksRepositoryTest
{
    private BooksRepository booksRepository;
    private AppDbContext dbContext;
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
    public async Task GetQueriedItemsAsync_ReturnsExistingBooks()
    {
        var newBook = DataGenerator.CreateTestBook(1);
        var newBook2 = DataGenerator.CreateTestBook(2);
        await dbContext.AddAsync(newBook);
        await dbContext.AddAsync(newBook2);
        await dbContext.SaveChangesAsync();
        var query = A.Dummy<Query>();
        var results = await booksRepository.GetQueriedItemsAsync(query);
        Assert.That(results.Count, Is.EqualTo(2));
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