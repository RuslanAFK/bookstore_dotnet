namespace BookStoreServer.Controllers.Resources.Books;

public class GetBooksResource
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}