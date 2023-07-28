using Domain.Abstractions;
using Domain.StaticManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Services;

public class FileService : IFileStorageService
{
    private readonly IHostEnvironment _host;

    public FileService(IHostEnvironment host)
    {
        _host = host;
    }

    public async Task<string> StoreFileAndGetPath(IFormFile file)
    {        
        var fileName = FileManager.GenerateFileNameWithExtension(file);
        var filePath = GenerateFullPath(fileName);

        await FileManager.WriteDataToFile(file, filePath);

        return fileName;
    }

    public void DeleteFile(string fileName)
    {
        var filePath = GenerateFullPath(fileName);
        FileManager.DeleteFileIfExists(filePath);
    }

    private string GenerateFullPath(string name)
    {
        var directoryPath = FileManager.GenerateDirectoryPath(_host.ContentRootPath);
        FileManager.CreateDirectoryIfNotExists(directoryPath);
        return Path.Combine(directoryPath, name);
    }
}