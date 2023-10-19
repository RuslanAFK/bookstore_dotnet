namespace Data.Test.Extensions;

public class PaginatorTest
{
    private IQueryable<Book> books = null!;
    [SetUp]
    public void Setup()
    {
        books = DataGenerator.PopulateBooksQueryable();
    }
    [Theory]
    [TestCase(2, 2, 2)]
    [TestCase(1, 5, 4)]
    [TestCase(2, 3, 1)]
    [TestCase(2, 2, 2)]
    public void ApplyPagination_GetItemsOnPageWithMaxPageSize(int page, int maxPageSize, int expectedItemsCount)
    {
        var query = new Query()
        {
            Page = page, PageSize = maxPageSize
        };
        var results = books.ApplyPagination(query);
        Assert.That(results.Count(), Is.EqualTo(expectedItemsCount));
    }
}