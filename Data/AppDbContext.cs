using System.Reflection;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public sealed class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<BookFile> BookFiles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    public AppDbContext(DbContextOptions options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}