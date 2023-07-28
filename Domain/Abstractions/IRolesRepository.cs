using Domain.Models;

namespace Domain.Abstractions;

public interface IRolesRepository
{
    Task<string> GetRoleNameByIdAsync(int id);
    Task AssignToRole(User user, string name);
}