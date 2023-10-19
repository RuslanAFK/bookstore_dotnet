namespace Services.ResponseDtos;

public class AuthResult
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Role { get; set; } = null!;
}