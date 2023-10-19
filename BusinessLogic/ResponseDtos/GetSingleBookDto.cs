namespace Services.ResponseDtos;

public class GetSingleBookDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Info { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string? BookFile { get; set; }
}