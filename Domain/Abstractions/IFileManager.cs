using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions;

public interface IFileManager
{
    Task<string> StoreFileAndGetPath(IFormFile file);
    void DeleteFile(string fileName);

}