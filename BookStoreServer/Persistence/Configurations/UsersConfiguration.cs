using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreServer.Persistence.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name)
            .HasMaxLength(16)
            .HasColumnName("Username");

        builder.HasIndex(u => u.Name)
            .IsUnique();
    }
}