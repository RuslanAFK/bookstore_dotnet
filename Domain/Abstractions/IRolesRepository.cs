using Domain.Models;

namespace Domain.Abstractions;

public interface IRolesRepository
{
    Task<string> GetRoleNameByIdAsync(int id);
    Task AssignToRoleAsync(User user, string name);
}