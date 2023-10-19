using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Resources.Auth;

public class RegisterResource
{
    [Required, MaxLength(16), MinLength(3)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(16), MinLength(3)]
    public string Password { get; set; } = null!;
}