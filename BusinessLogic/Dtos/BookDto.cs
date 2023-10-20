using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Services.Dtos;

public class BookDto
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
    
    public Book ToBook()
    {
        return new Book
        {
            Name = Name,
            Info = Info,
            Genre = Genre,
            Image = Image,
            Author = Author
        };
    }
}