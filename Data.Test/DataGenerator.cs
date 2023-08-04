using System.Xml.Linq;

namespace Data.Test;

public static class DataGenerator
{
    public static IQueryable<Book> PopulateBooksQueryable()
    {
        var enumerable = new List<Book>()
        {
            new() { Id = A.Dummy<int>(), Name = "Book1" },
            new() { Id = A.Dummy<int>(), Name = "Book2" },
            new() { Id = A.Dummy<int>(), Name = "Book3" },
            new() { Id = A.Dummy<int>(), Name = "Book4" }
        };
        return new EnumerableQuery<Book>(enumerable);
    }
    public static DbContextOptions<AppDbContext> CreateNewInMemoryDatabase()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    public static BookFile CreateTestBookFile()
    {
        return new BookFile
        {
            BookId = 11,
            Id = 1,
            Url = ""
        };
    }

    public static Book CreateTestBook(int id = 0)
    {
        return new Book
        {
            Id = id,
            BookFile = new BookFile()
            {
                Id = 0,
                Url = ""
            },
            Author = "",
            Genre = "",
            Image = "",
            Info = "",
            Name = "",
        };
    }

    public static Role CreateTestRole(int id = 0, string name = "")
    {
        return new Role
        {
            RoleId = id,
            RoleName = name
        };
    }
    public static User CreateTestUser(int id = 0, string name = "Giovanni")
    {
        return new User
        {
            Id = id,
            Name = name,
            Password = "",
            Role = new Role()
            {
                RoleId = 1,
                RoleName = "Role"
            },
        };
    }
    public static User CreateTestUserWithoutRole(int id = 0, string name = "Giovanni")
    {
        return new User
        {
            Id = id,
            Name = name,
            Password = "",
            RoleId = 1
        };
    }
}