using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Books;

public class UpdateBookResource
{
    [Required] 
    public int Id { get; set; }
    [Required, MaxLength(36), MinLength(3)]
    public string Name { get; set; }
    [Required, MaxLength(400), MinLength(10)]
    public string Info { get; set; }
    [Required, MaxLength(36), MinLength(3)]
    public string Genre { get; set; }
    [Required, FileExtensions, Url]
    public string Image { get; set; }
    [Required, MaxLength(36), MinLength(3)]
    public string Author { get; set; }
}