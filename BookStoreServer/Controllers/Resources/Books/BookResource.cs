using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Books;

public class CreateBookResource
{
    [Required, MaxLength(36), MinLength(3)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(400), MinLength(10)]
    public string Info { get; set; } = null!;

    [Required, MaxLength(36), MinLength(3)]
    public string Genre { get; set; } = null!;

    [Required, FileExtensions, Url]
    public string Image { get; set; } = null!;

    [Required, MaxLength(36), MinLength(3)]
    public string Author { get; set; } = null!;
}