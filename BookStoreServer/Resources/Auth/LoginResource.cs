using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace BookStoreServer.Resources.Auth;

public class LoginResource
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
            Password = Password
        };
    }
}