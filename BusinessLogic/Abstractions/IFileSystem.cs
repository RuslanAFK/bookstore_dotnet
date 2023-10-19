using Microsoft.AspNetCore.Http;

namespace Services.Abstractions;

public interface IFileSystem
{
    void CreateDirectoryIfNotExists(string path);
    void DeleteFileIfExists(string path);
    Task CreateFileAndWriteDataToItAsync(IFormFile file, string filePath);
}