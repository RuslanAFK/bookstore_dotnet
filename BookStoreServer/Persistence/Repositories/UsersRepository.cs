using BookStoreServer.Core;
using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Persistence.Repositories
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

        public async Task<User?> LoginAsync(User userToLogin)
        {
            var userFound = await _context.Users.SingleOrDefaultAsync(user =>
                user.Username == userToLogin.Username && user.Password == userToLogin.Password);
            return userFound;
        }
    }
}