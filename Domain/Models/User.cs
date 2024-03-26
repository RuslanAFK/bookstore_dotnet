namespace Domain.Models;

public class User : ISearchable
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Age { get; set; }
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
}