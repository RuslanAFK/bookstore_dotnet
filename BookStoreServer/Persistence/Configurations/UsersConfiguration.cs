using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreServer.Persistence.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username)
            .HasMaxLength(16);
        builder.Property(u => u.Password)
            .HasMaxLength(16);
        
        builder.HasIndex(u => u.Username)
            .IsUnique();
    }
}