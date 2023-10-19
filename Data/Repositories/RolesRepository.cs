﻿using System.Linq.Expressions;
using Data.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RolesRepository : BaseRepository<Role>, IRolesRepository
{
    public RolesRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<string> GetRoleNameByIdAsync(int id)
    {
        var role = await GetWhere(role => id == role.RoleId);
        var notNullRole = GetItemOrThrowNullError(role, id.ToString(), nameof(id));
        return notNullRole.RoleName;
    }

    public async Task AssignToRoleAsync(User user, string name)
    {
        var role = await GetWhere(r => r.RoleName == name);
        user.Role = GetItemOrThrowNullError(role, name, nameof(name));
    }

    private async Task<Role?> GetWhere(Expression<Func<Role, bool>> predicate)
    {
        var roles = GetAll();
        return await roles.SingleOrDefaultAsync(predicate);
    }
}