using Domain.Models;

namespace Services.Dtos;

public class GetUsersDto : ISearchable
{
    public int Id { get; set; }
    public string Name => Username;
    public string Username { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}