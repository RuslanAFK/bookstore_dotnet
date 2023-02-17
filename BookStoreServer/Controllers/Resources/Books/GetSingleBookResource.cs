namespace BookStoreServer.Controllers.Resources.Books;

public class GetSingleBookResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Info { get; set; }
    public string Genre { get; set; }
    public string Image { get; set; }
    public string Author { get; set; }
    public string? BookFile { get; set; }
}