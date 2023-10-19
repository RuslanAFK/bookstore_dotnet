using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Resources.Users;

public class DeleteUserResource
{
    [Required]
    public string Password { get; set; } = null!;
}