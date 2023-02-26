using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Auth;

public class RegisterResource
{
    [Required, MaxLength(16), MinLength(3)]
    public string Username { get; set; }
    [Required, MaxLength(16), MinLength(3)]
    public string Password { get; set; }
}