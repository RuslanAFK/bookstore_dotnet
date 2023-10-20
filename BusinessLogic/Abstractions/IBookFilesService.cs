using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Services.Abstractions;

public interface IBookFilesService
{
    Task AddAsync(int bookId, IFormFile file);
    Task RemoveAsync(int bookId);
    void ThrowIfFileNotPermitted(IFormFile file);
}