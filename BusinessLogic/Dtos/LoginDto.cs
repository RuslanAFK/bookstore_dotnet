using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Services.Dtos;

public class LoginDto
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public User ToUser()
    {
        return new User
        {
            Name = Username,
            Email = Username,
            Password = Password
        };
    }
}