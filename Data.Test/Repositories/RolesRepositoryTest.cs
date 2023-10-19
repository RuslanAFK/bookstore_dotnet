namespace Data.Test.Repositories;

public class RolesRepositoryTest
{
    private RolesRepository repository = null!;
    private AppDbContext dbContext = null!;
    [SetUp]
    public void Setup()
    {
        var options = DataGenerator.CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        repository = new RolesRepository(dbContext);
    }
    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
}