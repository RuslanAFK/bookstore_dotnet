using BookStoreServer.Core.Services;

namespace BookStoreServer.Persistence.Services;

public class FileStorage : IFileStorage
{
    public async Task<string> StoreFile(string uploadsPath, IFormFile file)
    {
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);
        
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsPath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }

    public void DeleteFile(string uploadsPath, string fileName)
    {
        var filePath = Path.Combine(uploadsPath, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}