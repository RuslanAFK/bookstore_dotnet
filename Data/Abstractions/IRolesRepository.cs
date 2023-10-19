namespace Data.Abstractions;

public interface IRolesRepository
{
    Task<int> GetRoleIdByNameAsync(string name);
}