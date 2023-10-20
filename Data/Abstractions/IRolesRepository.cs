using Domain.Models;

namespace Data.Abstractions;

public interface IRolesRepository
{
    Task<Role> GetByNameAsync(string name);
}