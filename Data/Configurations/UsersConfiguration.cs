using Domain.Models;
using Domain.StaticManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name)
            .HasMaxLength(16)
            .HasColumnName("Username");

        builder.HasIndex(u => u.Name)
            .IsUnique();

        var user = new User
        {
            Name = "superuser", Password = "superuser", RoleId = 3, Id = 1
        };
        var securedUser = PasswordManager.GetSecuredUser(user);

        builder.HasData(securedUser);
    }
}