namespace Data.Test.Repositories;

public class BookFilesRepositoryTest
{
    private BookFileRepository repository;
    private AppDbContext dbContext;
    [SetUp]
    public void Setup()
    {
        var options = DataGenerator.CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        repository = new BookFileRepository(dbContext);
    }
    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
    [Test]
    public async Task AddAsync_CreatesNewBookFileInsideDb()
    {
        var newItem = DataGenerator.CreateTestBookFile();
        await repository.AddAsync(newItem);
        await dbContext.SaveChangesAsync();

        var foundItem = await dbContext.BookFiles.FindAsync(newItem.Id);
        Assert.That(foundItem, Is.Not.Null);
    }
    [Test]
    public async Task Update_ChangesBookFile()
    {
        var newUrl = "newUrl";
        var newItem = DataGenerator.CreateTestBookFile();
        await dbContext.AddAsync(newItem);
        await dbContext.SaveChangesAsync();

        newItem.Url = newUrl;
        repository.Update(newItem);
        await dbContext.SaveChangesAsync();

        var foundItem = await dbContext.BookFiles.FindAsync(newItem.Id);
        var actualUrl = foundItem?.Url;
        Assert.That(actualUrl, Is.EqualTo(newUrl));
    }
    [Test]
    public async Task Remove_DeletesBookFile()
    {
        var newItem = DataGenerator.CreateTestBookFile();
        await dbContext.AddAsync(newItem);
        await dbContext.SaveChangesAsync();

        repository.Remove(newItem);
        await dbContext.SaveChangesAsync();

        var foundItem = await dbContext.BookFiles.FindAsync(newItem.Id);
        Assert.That(foundItem, Is.Null);
    }
}