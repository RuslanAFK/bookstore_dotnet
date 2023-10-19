namespace Data.Test.Repositories;

public class UnitOfWorkTest
{
    private AppDbContext dbContext = null!;
    private UnitOfWork unitOfWork = null!;
    [SetUp]
    public void Setup()
    {
        var options = DataGenerator.CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        unitOfWork = new UnitOfWork(dbContext);
    }
    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
    [Test]
    public void CompleteOrThrowAsync_NothingChangedToDb_ThrowsOperationNotSuccessfulException()
    {
        Assert.ThrowsAsync<OperationNotSuccessfulException>(async () =>
        {
            await unitOfWork.CompleteAsync();
        });
    }
    [Test]
    public void CompleteOrThrowAsync_ChangedDbWithCorrectData_DoesNotThrow()
    {
        var role = DataGenerator.CreateTestRole();
        Assert.DoesNotThrowAsync(async () =>
        {
            await dbContext.AddAsync(role);
            await unitOfWork.CompleteAsync();
        });
    }
    [Test]
    public void CompleteOrThrowAsync_ChangeDbWithWrongData_ThrowsOperationNotSuccessfulException()
    {
        var dummyRole = A.Dummy<Role>();
        Assert.ThrowsAsync<OperationNotSuccessfulException>(async () =>
        {
            await dbContext.AddAsync(dummyRole);
            await unitOfWork.CompleteAsync();
        });
    }
}