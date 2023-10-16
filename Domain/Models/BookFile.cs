namespace Domain.Models;

public class BookFile
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;
    public int BookId { get; set; }
    public virtual Book? Book { get; set; }
}