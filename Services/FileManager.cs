using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Services;

public class FileManager : IFileManager
{
    private readonly IHostEnvironment _host;
    private readonly IFileSystem _fileSystem;

    public FileManager(IHostEnvironment host, IFileSystem fileSystem)
    {
        _host = host;
        _fileSystem = fileSystem;
    }

    public async Task<string> StoreFileAndGetPath(IFormFile file)
    {        
        var fileName = GenerateFileNameWithExtension(file);
        var filePath = GenerateFullPath(fileName);
        await _fileSystem.CreateFileAndWriteDataToItAsync(file, filePath);
        return fileName;
    }

    public void DeleteFile(string fileName)
    {
        var filePath = GenerateFullPath(fileName);
        _fileSystem.DeleteFileIfExists(filePath);
    } 

    private string GenerateFullPath(string name)
    {
        var directoryPath = GenerateDirectoryPath();
        _fileSystem.CreateDirectoryIfNotExists(directoryPath);
        return Path.Combine(directoryPath, name);
    }

    private string GenerateDirectoryPath()
    {
        string contentRoot = _host.ContentRootPath;
        return Path.Combine(contentRoot, "wwwroot", "uploads");
    }

    private string GenerateFileNameWithExtension(IFormFile file)
    {
        var randomName = Guid.NewGuid();
        var extension = Path.GetExtension(file.FileName);
        return randomName + extension;
    }
}