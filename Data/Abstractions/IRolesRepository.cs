using Domain.Models;

namespace Data.Abstractions;

public interface IRolesRepository
{
    Task<string> GetRoleNameByIdAsync(int id);
    Task AssignToRoleAsync(User user, string name);
}