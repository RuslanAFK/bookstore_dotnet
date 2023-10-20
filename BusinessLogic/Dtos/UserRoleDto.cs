using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class UserRoleDto
{
    [Required]
    public string RoleName { get; set; } = null!;
}