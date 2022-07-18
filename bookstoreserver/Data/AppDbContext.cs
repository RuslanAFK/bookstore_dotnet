using Microsoft.EntityFrameworkCore;

namespace bookstoreserver.Data
{
    internal sealed class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder x) => x.UseSqlite("Data Source=./Data/AppDb.db");

    }
}
