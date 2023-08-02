using Data.Repositories;
using Domain.Models;

namespace Data.Test.Repositories;

public class BookFilesRepositoryTest
{
    private DbContextOptions<AppDbContext> options;
    private BookFileRepository repository;
    private AppDbContext dbContext;

    [SetUp]
    public void Setup()
    {
        options = CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        repository = new BookFileRepository(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
    private DbContextOptions<AppDbContext> CreateNewInMemoryDatabase()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [Test]
    public async Task AddAsync_CreatesNewInstanceInsideDb()
    {
        var bookFile = new BookFile
        {
            BookId = 11,
            Id = 1,
            Url = ""
        };
        await repository.AddAsync(bookFile);
        await dbContext.SaveChangesAsync();

        var fileFound = await dbContext.BookFiles.FindAsync(bookFile.Id);
        Assert.IsNotNull(fileFound);
    }
    [Test]
    public async Task Update_ChangesValues()
    {
        var newUrl = "newUrl";
        var bookFile = new BookFile
        {
            BookId = 55,
            Id = 1,
            Url = ""
        };
        await dbContext.AddAsync(bookFile);
        await dbContext.SaveChangesAsync();

        bookFile.Url = newUrl;
        repository.Update(bookFile);
        await dbContext.SaveChangesAsync();

        var fileFound = await dbContext.BookFiles.FindAsync(bookFile.Id);
        Assert.That(fileFound.Url, Is.EqualTo(newUrl));
    }
    [Test]
    public async Task Remove_DeletesData()
    {
        var bookFile = new BookFile
        {
            BookId = 55,
            Id = 1,
            Url = ""
        };
        await dbContext.AddAsync(bookFile);
        await dbContext.SaveChangesAsync();

        repository.Remove(bookFile);
        await dbContext.SaveChangesAsync();

        var fileFound = await dbContext.BookFiles.FindAsync(bookFile.Id);
        Assert.That(fileFound, Is.Null);
    }
}