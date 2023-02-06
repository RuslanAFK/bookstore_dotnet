using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Persistence.Services;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Signup(User userToCreate)
    {
        _context.Users.Add(userToCreate);
    }

    public async Task<User?> CheckCredentialsAsync(User userToLogin)
    {
        var userFound = await _context.Users.SingleOrDefaultAsync(user =>
            user.Name == userToLogin.Name);
        return userFound;
    }

    public async Task<string> GetUserRole(int roleId)
    {
        var role = await _context.Roles.FindAsync(roleId);
        return role.RoleName;
    }

    public async Task AddToRole(User user, bool isAdmin)
    {
        var roleName = isAdmin ? "Admin" : "User";
            
        var role = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == roleName);
        user.Role = role;
    }
}