using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Resources.Auth;

public class LoginResource
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}