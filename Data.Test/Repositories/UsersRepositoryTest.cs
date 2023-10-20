namespace Data.Test.Repositories;

public class UsersRepositoryTest
{
    private UsersRepository repository = null!;
    private AppDbContext dbContext = null!;
    [SetUp]
    public void Setup()
    {
        var options = DataGenerator.CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        repository = new UsersRepository(dbContext);
    }
    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
  
    [Test]
    public async Task GetByNameAsync_WithExistingName_ReturnsSameUser()
    {
        var user = DataGenerator.CreateTestUser();
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var results = await repository.GetByNameAsync(user.Name);
        var actualId = results.Id;
        var expectedId = user.Id;
        Assert.That(actualId, Is.EqualTo(expectedId));
    }
    [Test]
    public void GetByNameAsync_WithNonExistingName_ThrowsEntityNotFoundException()
    {
        var name = A.Dummy<string>();
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.GetByNameAsync(name);
        });
    }
    [Test]
    public async Task AddAsync_NoDuplicate_AddsUsers()
    {
        var user = DataGenerator.CreateTestUserWithoutRole(1);
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var user2 = DataGenerator.CreateTestUserWithoutRole(2, "Sabrina");
        await repository.AddAsync(user2);
        await dbContext.SaveChangesAsync();

        var storedUsersCount = dbContext.Users.Count();
        Assert.That(storedUsersCount, Is.EqualTo(2));
    }
    [Test]
    public async Task AddAsync_WithDuplicate_ThrowsEntityAlreadyExistsException()
    {
        var user = DataGenerator.CreateTestUserWithoutRole(1);
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var user2 = DataGenerator.CreateTestUserWithoutRole(2);

        Assert.ThrowsAsync<EntityAlreadyExistsException>(async () =>
        {
            await repository.AddAsync(user2);
        });
    }
}