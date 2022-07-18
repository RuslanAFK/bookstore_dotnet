using Microsoft.EntityFrameworkCore;

namespace bookstoreserver.Data
{
    public class UsersRepository
    {
        internal async static Task<List<User>> GetUsersAsync()
        {
            using (var db = new AppDbContext())
            {
                return await db.Users.ToListAsync();
            }
        }
        internal async static Task<User> GetUserByIdAsync(int userId)
        {
            using (var db = new AppDbContext())
            {
                return await db.Users.FirstOrDefaultAsync(user => user.Id == userId);
            }
        }
        internal async static Task<bool> SignupAsync(User userToCreate)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var userFound = await db.Users.FirstOrDefaultAsync(user => user.Username == userToCreate.Username);
                    if(userFound != null)
                    {
                        return false;
                    }
                    await db.Users.AddAsync(userToCreate);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        internal async static Task<User> LoginAsync(User userToLogin)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var userFound = await db.Users.FirstOrDefaultAsync(user => user.Username == userToLogin.Username
                        &&user.Password == userToLogin.Password);
                    return userFound;
                    
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}
