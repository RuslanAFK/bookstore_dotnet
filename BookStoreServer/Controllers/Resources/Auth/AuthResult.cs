namespace BookStoreServer.Controllers.Resources.Auth;

public class AuthResult
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}