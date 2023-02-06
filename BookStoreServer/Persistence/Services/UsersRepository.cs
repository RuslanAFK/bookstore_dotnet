using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
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

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
        }
    }
}