using Data.Repositories;
using Domain.Exceptions;
using Domain.Models;

namespace Data.Test.Repositories;

public class UnitOfWorkTest
{
    private DbContextOptions<AppDbContext> options;
    private AppDbContext dbContext;
    private UnitOfWork unitOfWork;

    [SetUp]
    public void Setup()
    {
        options = CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        unitOfWork = new UnitOfWork(dbContext);
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
    public void CompleteOrThrowAsync_NothingChangedToDb_ThrowsOperationNotSuccessfulException()
    {
        Assert.ThrowsAsync<OperationNotSuccessfulException>(async () =>
        {
            await unitOfWork.CompleteOrThrowAsync();
        });
    }
    [Test]
    public void CompleteOrThrowAsync_ChangedDbWithCorrectData_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await dbContext.AddAsync(new Role
            {
                RoleId = 1,
                RoleName = ""
            });
            await unitOfWork.CompleteOrThrowAsync();
        });
    }
    [Test]
    public void CompleteOrThrowAsync_ChangeDbWithCorruptedData_ThrowsOperationNotSuccessfulException()
    {
        Assert.ThrowsAsync<OperationNotSuccessfulException>(async () =>
        {
            await dbContext.AddAsync(new Role
            {
                RoleId = 1,
            });
            await unitOfWork.CompleteOrThrowAsync();
        });
    }
}