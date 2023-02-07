using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using BookStoreServer.Enums;
using BookStoreServer.Extensions;
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

        public async Task<ListResponse<User>> GetUsersAsync(QueryObject queryObject)
        {
            var users = _context.Users.Include(user => user.Role)
                .ApplySearching(queryObject);
            var response = new ListResponse<User>()
            {
                Count = users.Count(),
                Items = await users.ApplyPagination(queryObject).ToListAsync()
            };
            return response;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
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

        public async Task<string> GetRoleById(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return role.RoleName;
        }

        public async Task AddUserToRole(User user, bool isAdmin)
        {
            var roleName = isAdmin ? Roles.Admin : Roles.User;
            
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == roleName);
            user.Role = role;
        }
    }
}