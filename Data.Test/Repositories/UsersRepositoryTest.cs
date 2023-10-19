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
    public async Task GetByIdIncludingRolesAsync_WithExistingId_ReturnsSameUser()
    {
        var id = 5;
        var user = DataGenerator.CreateTestUser(id);
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var results = await repository.GetByIdIncludingRolesAsync(id);
        var actualName = results.Name;
        var expectedName = user.Name;
        Assert.That(actualName, Is.EqualTo(expectedName));
    }
    [Test]
    public async Task GetByIdIncludingRolesAsync_WithExistingId_ReturnsUserWithSameRole()
    {
        var id = 5;
        var user = DataGenerator.CreateTestUser(id);
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var results = await repository.GetByIdIncludingRolesAsync(id);
        var actualRoleName = results.Role!.RoleName;
        var expectedRoleName = user.Role!.RoleName;
        Assert.That(actualRoleName, Is.EqualTo(expectedRoleName));
    }
    [Test]
    public void GetByIdIncludingRolesAsync_WithNonExistingId_ThrowsEntityNotFoundException()
    {
        var id = 5;
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.GetByIdIncludingRolesAsync(id);
        });
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
    public async Task GetByNameIncludingRolesAsync_WithExistingName_ReturnsCorrectUser()
    {
        var id = 5;
        var user = DataGenerator.CreateTestUser(id);
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var results = await repository.GetByNameIncludingRolesAsync(user.Name);
        var actualId = results.Id;
        var expectedId = user.Id;
        Assert.That(actualId, Is.EqualTo(expectedId));
    }
    [Test]
    public async Task GetByNameIncludingRolesAsync_WithExistingName_ReturnsSameUserRole()
    {
        var id = 5;
        var user = DataGenerator.CreateTestUser(id);
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var results = await repository.GetByNameIncludingRolesAsync(user.Name);
        var expectedRoleName = user.Role!.RoleName;
        var actualRoleName = results.Role!.RoleName;
        Assert.That(actualRoleName, Is.EqualTo(expectedRoleName));
    }
    [Test]
    public void GetByNameIncludingRolesAsync_WithWrongName_ThrowsEntityNotFoundException()
    {
        var name = A.Dummy<string>();
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.GetByNameIncludingRolesAsync(name);
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