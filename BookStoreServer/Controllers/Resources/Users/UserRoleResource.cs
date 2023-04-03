using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Users;

public class UserRoleResource
{
    [Required]
    public string RoleName { get; set; }
}