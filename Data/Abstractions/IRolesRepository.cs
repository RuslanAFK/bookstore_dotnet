using Domain.Models;

namespace Data.Abstractions;

public interface IRolesRepository
{
    Task AssignToRoleAsync(User user, string name);
}