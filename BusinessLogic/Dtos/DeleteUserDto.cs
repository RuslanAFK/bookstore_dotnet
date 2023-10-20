using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class DeleteUserDto
{
    [Required]
    public string Password { get; set; } = null!;
}