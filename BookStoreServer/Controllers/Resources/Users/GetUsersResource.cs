namespace BookStoreServer.Controllers.Resources.Users;

public class GetUsersResource
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}