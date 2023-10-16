namespace BookStoreServer.Controllers.Resources.Books;

public class ListResponseResource<T>
{
    public ICollection<T> Items { get; set; } = new List<T>();
    public int Count { get; set; }
}