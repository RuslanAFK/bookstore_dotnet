using Domain.Models;

namespace Services.Dtos;

public class GetBooksDto : ISearchable
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}