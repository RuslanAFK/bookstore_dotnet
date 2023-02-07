using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Users;

public class UpdateUserInfoResource
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    public string? NewPassword { get; set; }
    [Required]
    public string Password { get; set; }
}