using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Controllers.Resources;

public class RegisterResource
{
    [MaxLength(16)]
    public string Username { get; set; }
    [MaxLength(16)]
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}