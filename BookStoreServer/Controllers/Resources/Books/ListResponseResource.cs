namespace BookStoreServer.Controllers.Resources;

public class ListResponseResource<T>
{
    public ICollection<T> Items { get; set; }
    public int Count { get; set; }
}