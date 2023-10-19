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
    
    [Test]
    public async Task AssignToRoleAsync_WithCorrectRoleName_AssignsCorrectRole()
    {
        var id = 10;
        var name = "myRole";
        var role = DataGenerator.CreateTestRole(id, name);
        await dbContext.AddAsync(role);
        await dbContext.SaveChangesAsync();

        var user = A.Dummy<User>();
        await repository.AssignToRoleAsync(user, name);
        var actualRoleId = user.Role!.RoleId;
        Assert.That(actualRoleId, Is.EqualTo(id));
    }
    [Test]
    public void AssignToRoleAsync_WithWrongRoleName_ThrowsEntityNotFoundException()
    {
        var user = A.Dummy<User>();
        var roleName = A.Dummy<string>();
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.AssignToRoleAsync(user, roleName);
        });
        
    }
}