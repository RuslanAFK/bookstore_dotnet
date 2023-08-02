using Data.Repositories;
using Domain.Exceptions;
using Domain.Models;

namespace Data.Test.Repositories;

public class UsersRepositoryTest
{
    private DbContextOptions<AppDbContext> options;
    private UsersRepository repository;
    private AppDbContext dbContext;

    [SetUp]
    public void Setup()
    {
        options = CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        repository = new UsersRepository(dbContext);
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
    public async Task GetQueriedAsync_NothingInDb_ReturnsCount0()
    {
        var results = await repository.GetQueriedItemsAsync(A.Dummy<Query>());
        Assert.That(results.Count, Is.EqualTo(0));
    }
    [Test]
    public async Task GetQueriedAsync_OneInDb_ReturnsCount1()
    {
        var user = new User { Id = 1, Name = "", Password = "", RoleId = 1 };
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var results = await repository.GetQueriedItemsAsync(A.Dummy<Query>());
        Assert.That(results.Count, Is.EqualTo(1));
    }
    [Test]
    // TODO Fix the bug role not mandatory / checking if role exists
    public async Task GetByIdIncludingRolesAsync_WithCorrectId_ReturnsCorrectUser()
    {
        var id = 5;
        var user = new User
        {
            Id = id, Name = "Giovanni", Password = "", Role = new Role()
            {
                RoleId = 1,
                RoleName = "Role"
            },
        };

        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();


        var results = await repository.GetByIdIncludingRolesAsync(id);
        Assert.That(results.Name, Is.EqualTo(user.Name));
    }
    [Test]
    public async Task GetByIdIncludingRolesAsync_WithCorrectId_ReturnsUserWithCorrectRole()
    {
        var id = 5;
        var user = new User
        {
            Id = id,
            Name = "Giovanni",
            Password = "",
            Role = new Role()
            {
                RoleId = 1,
                RoleName = "Role"
            },
        };

        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();


        var results = await repository.GetByIdIncludingRolesAsync(id);
        Assert.That(results.Role.RoleName, Is.EqualTo(user.Role.RoleName));
    }
    [Test]
    public void GetByIdIncludingRolesAsync_WithWrongId_ThrowsEntityNotFoundException()
    {
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.GetByIdIncludingRolesAsync(55);
        });
    }

    [Test]
    public async Task GetByNameAsync_WithCorrectName_ReturnsCorrectUser()
    {
        var user = new User
        {
            Id = 5,
            Name = "Giovanni",
            Password = "",
            Role = new Role()
            {
                RoleId = 1,
                RoleName = "Role"
            },
        };

        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();


        var results = await repository.GetByNameAsync(user.Name);
        Assert.That(results.Id, Is.EqualTo(user.Id));
    }

    [Test]
    public void GetByNameAsync_WithWrongName_ThrowsEntityNotFoundException()
    {
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.GetByNameAsync("ejrvf");
        });
    }
    [Test]
    // TODO Fix the bug role not mandatory / checking if role exists
    public async Task GetByNameIncludingRolesAsync_WithCorrectName_ReturnsCorrectUser()
    {
        var id = 5;
        var user = new User
        {
            Id = id,
            Name = "Giovanni",
            Password = "",
            Role = new Role()
            {
                RoleId = 1,
                RoleName = "Role"
            },
        };

        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();


        var results = await repository.GetByNameIncludingRolesAsync(user.Name);
        Assert.That(results.Id, Is.EqualTo(user.Id));
    }
    [Test]
    public async Task GetByNameIncludingRolesAsync_WithCorrectName_ReturnsUserWithCorrectRole()
    {
        var id = 5;
        var user = new User
        {
            Id = id,
            Name = "Giovanni",
            Password = "",
            Role = new Role()
            {
                RoleId = 1,
                RoleName = "Role"
            },
        };

        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();


        var results = await repository.GetByNameIncludingRolesAsync(user.Name);
        Assert.That(results.Role.RoleName, Is.EqualTo(user.Role.RoleName));
    }
    [Test]
    public void GetByNameIncludingRolesAsync_WithWrongName_ThrowsEntityNotFoundException()
    {
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await repository.GetByNameIncludingRolesAsync("kdhv");
        });
    }


}