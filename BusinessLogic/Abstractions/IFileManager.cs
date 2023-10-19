using Microsoft.AspNetCore.Http;

namespace Services.Abstractions;

public interface IFileManager
{
    Task<string> StoreFileAndGetPath(IFormFile file);
    void DeleteFile(string fileName);

}