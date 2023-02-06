namespace BookStoreServer.Controllers.Resources;

public class AuthResult
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}