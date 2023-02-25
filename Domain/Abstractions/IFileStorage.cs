using Microsoft.AspNetCore.Http;

namespace BookStoreServer.Core.Services;

public interface IFileStorage
{
    Task<string> StoreFile(string uploadsPath, IFormFile file);
    void DeleteFile(string uploadsPath, string fileName);

}