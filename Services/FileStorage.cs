using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Services;

public class FileSystem : IFileSystem
{
    public void CreateDirectoryIfNotExists(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    public void DeleteFileIfExists(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
    }

    public async Task CreateFileAndWriteDataToItAsync(IFormFile file, string filePath)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }
}