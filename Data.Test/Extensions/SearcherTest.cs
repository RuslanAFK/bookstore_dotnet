namespace Data.Test.Extensions;

public class SearcherTest
{
    private IQueryable<Book> books;
    [SetUp]
    public void Setup()
    {
        books = DataGenerator.PopulateBooksQueryable();
    }
    [Theory]
    [TestCase("book", 4)]
    [TestCase("3", 1)]
    [TestCase("oko", 0)]
    public void ApplySearching_GetItemsIncludingLiteral(string literal, int itemsCount)
    {
        var query = new Query() { Search = literal };
        var results = books.ApplySearching(query);
        Assert.That(results.Count(), Is.EqualTo(itemsCount));
    }
}