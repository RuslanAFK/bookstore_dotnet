using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions;

public interface IFileStorageService
{
    Task<string> StoreFileAndGetPath(IFormFile file);
    void DeleteFile(string fileName);

}