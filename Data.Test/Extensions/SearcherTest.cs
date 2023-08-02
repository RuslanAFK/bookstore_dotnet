using Data.Extensions;
using Domain.Models;

namespace Data.Test.Extensions;

public class SearcherTest
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
    public void ApplySearching_SearchForBooksWithBookWord_Returns4()
    {
        var query = new Query() { Search = "book" };
        var results = books.ApplySearching(query);
        Assert.That(results.Count(), Is.EqualTo(4));
    }
    [Test]
    public void ApplySearching_SearchForBooksWith3kWord_Returns1()
    {
        var query = new Query() { Search = "3" };
        var results = books.ApplySearching(query);
        Assert.That(results.Count(), Is.EqualTo(1));
    }
}