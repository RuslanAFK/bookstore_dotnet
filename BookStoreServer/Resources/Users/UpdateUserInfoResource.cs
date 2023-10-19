using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace BookStoreServer.Resources.Users;

public class UpdateUserInfoResource
{
    [Required, MaxLength(16), MinLength(3)]
    public string Username { get; set; } = null!;

    [MaxLength(16), MinLength(3)]
    public string? NewPassword { get; set; }
    [Required]
    public string Password { get; set; } = null!;
    
    public User ToUser()
    {
        return new User
        {
            Name = Username,
            Password = Password
        };
    }
}