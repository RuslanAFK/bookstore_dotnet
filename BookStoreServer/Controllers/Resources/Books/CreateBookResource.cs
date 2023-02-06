using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources;

public class CreateBookResource
{
    [Required, MaxLength(36)]
    public string Name { get; set; }
    [Required]
    public string Info { get; set; }
    [Required, MaxLength(36)]
    public string Genre { get; set; }
    [Required]
    public string Image { get; set; }
    [Required, MaxLength(36)]
    public string Author { get; set; }
}