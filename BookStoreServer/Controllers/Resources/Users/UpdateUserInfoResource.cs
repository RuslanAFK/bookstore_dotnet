using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Users;

public class UpdateUserInfoResource
{
    [Required, MaxLength(16), MinLength(3)]
    public string Username { get; set; }
    [MaxLength(16), MinLength(3)]
    public string? NewPassword { get; set; }
    [Required]
    public string Password { get; set; }
}