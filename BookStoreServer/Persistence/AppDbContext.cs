using System.Reflection;
using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Persistence
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
