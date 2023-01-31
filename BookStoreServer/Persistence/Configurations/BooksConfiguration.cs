using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreServer.Persistence.Configurations;

public class BooksConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(u => u.Name)
            .HasMaxLength(36);
        builder.Property(u => u.Genre)
            .HasMaxLength(36);
        builder.Property(u => u.Author)
            .HasMaxLength(36);
    }
}