using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Services.Dtos;

public class RegisterDto
{
    [Required, MaxLength(16), MinLength(5)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(16), MinLength(8)]
    public string Password { get; set; } = null!;
    
    [Required]
    public string Email { get; set; } = null!;
    
    [Required, Range(1, 120)]
    public int Age { get; set; }
    
    public User ToUser()
    {
        return new User
        {
            Name = Username,
            Password = Password,
            Email = Email,
            Age = Age
        };
    }
}