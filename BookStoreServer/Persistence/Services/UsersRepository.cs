using BookStoreServer.Core;
using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Persistence.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
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
                user.Username == userToLogin.Username);
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
}