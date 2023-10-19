using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Resources.Users;

public class UserRoleResource
{
    [Required]
    public string RoleName { get; set; } = null!;
}