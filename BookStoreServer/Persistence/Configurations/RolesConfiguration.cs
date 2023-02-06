using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreServer.Persistence.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role { RoleId = 1, RoleName = "User" });
        builder.HasData(new Role { RoleId = 2, RoleName = "Admin" });
        builder.HasData(new Role { RoleId = 3, RoleName = "Creator" });
    }
}