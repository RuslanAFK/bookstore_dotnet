namespace Domain.Models;

public class AuthResult
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }

    public AuthResult(User user, string token, string roleName)
    {
        Id = user.Id;
        Username = user.Name;
        Token = token;
        Role = roleName;
    }
}