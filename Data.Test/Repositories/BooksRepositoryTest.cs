using Data.Repositories;
using Domain.Exceptions;
using Domain.Models;

namespace Data.Test.Repositories;

public class BooksRepositoryTest
{
    private DbContextOptions<AppDbContext> options;
    private BooksRepository booksRepository;
    private AppDbContext dbContext;

    [SetUp]
    public void Setup()
    {
        options = CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        booksRepository = new BooksRepository(dbContext);
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
    public async Task GetQueriedItemsAsync_ReturnsDbExistentCountOfBooks()
    {
        var id = 99;
        await dbContext.Books.AddAsync(new Book
        {
            Id = id,
            BookFile = new BookFile()
            {
                Id = default,
                Url = ""
            },
            Author = "",
            Genre = "",
            Image = "",
            Info = "",
            Name = "",
        });
        await dbContext.SaveChangesAsync();
        var results = await booksRepository.GetQueriedItemsAsync(A.Dummy<Query>());
        Assert.That(results.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetIncludingBookFilesAsync_WithCorrectId_ReturnsBookWithNoNullBookFile()
    {
        var id = 99;
        await dbContext.Books.AddAsync(new Book
        {
            Id = id,
            BookFile = new BookFile()
            {
                Id = default,
                Url = ""
            },
            Author = "",
            Genre = "",
            Image = "",
            Info = "",
            Name = "",
        });
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