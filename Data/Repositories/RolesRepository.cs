using Data.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RolesRepository : BaseRepository<Role>, IRolesRepository
{
    public RolesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> GetRoleIdByNameAsync(string name)
    {
        var role = await GetAll()
            .Where(x => x.RoleName == name)
            .Select(x => new
            {
                x.RoleId
            })
            .FirstOrDefaultAsync() ?? throw new EntityNotFoundException(typeof(Role), nameof(Role.RoleName));
        return role.RoleId;
    }
}