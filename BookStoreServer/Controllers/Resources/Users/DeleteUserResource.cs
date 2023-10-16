using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Users;

public class DeleteUserResource
{
    [Required]
    public string Password { get; set; } = null!;
}