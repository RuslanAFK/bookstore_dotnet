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

    public async Task<Role> GetByNameAsync(string name)
    {
        return await GetAll()
            .Where(x => x.RoleName == name)
            .FirstOrDefaultAsync() ?? throw new EntityNotFoundException(typeof(Role), nameof(Role.RoleName));
    }
}