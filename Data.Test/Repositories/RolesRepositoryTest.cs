using Data.Repositories;
using Domain.Exceptions;
using Domain.Models;

namespace Data.Test.Repositories;

public class RolesRepositoryTest
{
    private DbContextOptions<AppDbContext> options;
    private RolesRepository repository;
    private AppDbContext dbContext;

    [SetUp]
    public void Setup()
    {
        options = CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        repository = new RolesRepository(dbContext);
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
    public async Task GetRoleNameByIdAsync_WithCorrectId_ReturnsName()
    {
        var id = 10;
        var name = "myRole";
        var role = new Role
        {
            RoleId = id,
            RoleName = name
        };
        await dbContext.AddAsync(role);
        await dbContext.SaveChangesAsync();

        var roleNameFound = await repository.GetRoleNameByIdAsync(id);
        Assert.That(roleNameFound, Is.EqualTo(name));
    }
    [Test]
    public void GetRoleNameByIdAsync_WithWrongId_ThrowsEntityNotFoundException()
    {
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.GetRoleNameByIdAsync(10);
        });
    }
    [Test]
    public async Task AssignToRoleAsync_WithCorrectRoleName_AssignsCorrectRole()
    {
        var id = 10;
        var name = "myRole";
        var role = new Role
        {
            RoleId = id,
            RoleName = name
        };
        await dbContext.AddAsync(role);
        await dbContext.SaveChangesAsync();
        var user = A.Dummy<User>();


        await repository.AssignToRoleAsync(user, name);
        Assert.That(user.Role.RoleId, Is.EqualTo(id));
    }
    [Test]
    public void AssignToRoleAsync_WithWrongRoleName_ThrowsEntityNotFoundException()
    {
        var user = A.Dummy<User>();

        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.AssignToRoleAsync(user, "dummyName");
        });
        
    }
}