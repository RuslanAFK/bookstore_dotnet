using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Services.Abstractions;

public interface IBookFilesService
{
    Task AddAsync(Book book, IFormFile file);
    Task RemoveAsync(Book book);
    void ThrowIfFileNotPermitted(IFormFile file);
}