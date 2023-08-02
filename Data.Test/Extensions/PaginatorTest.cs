using Data.Extensions;
using Domain.Models;

namespace Data.Test.Extensions;

public class PaginatorTest
{
    private IQueryable<Book> books;

    [SetUp]
    public void Setup()
    {
        var enumerable = new List<Book>()
        {
            new Book()
            {
                Id = default, Name = "Book1"
            },
            new Book()
            {
                Id = default, Name = "Book2"
            },
            new Book()
            {
                Id = default, Name = "Book3"
            },
            new Book()
            {
                Id = default, Name = "Book4"
            }
        };
        books = new EnumerableQuery<Book>(enumerable);
    }

    [Test]
    public void ApplyPagination_WithPageSize2Existent_Returns2Items()
    {
        var query = new Query()
        {
            Page = 2, PageSize = 2
        };
        var results = books.ApplyPagination(query);
        Assert.That(results.Count(), Is.EqualTo(2));
    }
    [Test]
    public void ApplyPagination_WithPageSize4ExistentOutta5_Returns4Items()
    {
        var query = new Query()
        {
            Page = 1,
            PageSize = 5
        };
        var results = books.ApplyPagination(query);
        Assert.That(results.Count(), Is.EqualTo(4));
    }
}