namespace BookStoreServer.Resources.Auth;

public class AuthResultResource
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Role { get; set; } = null!;
}