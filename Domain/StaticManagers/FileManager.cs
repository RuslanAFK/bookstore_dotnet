using Microsoft.AspNetCore.Http;

namespace Domain.StaticManagers;

public static class FileManager
{
    public static void CreateDirectoryIfNotExists(string uploadsPath)
    {
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);
    }
    public static void DeleteFileIfExists(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
    public static async Task WriteDataToFile(IFormFile file, string filePath)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }

    public static string GenerateDirectoryPath(string contentRoot)
    {
        return Path.Combine(contentRoot, "wwwroot", "uploads");
    }

    public static string GenerateFileNameWithExtension(IFormFile file)
    {
        var randomName = Guid.NewGuid();
        var extension = Path.GetExtension(file.FileName);
        return randomName + extension;
    }
}