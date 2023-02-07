using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources.Users;

public class UpdateUserRoleResource
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string RoleName { get; set; }
}