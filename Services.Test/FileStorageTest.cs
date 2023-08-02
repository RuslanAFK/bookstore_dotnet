using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Services.Test;

public class FileStorageTest
{
    private string tempDirectory;
    private FileSystem fileSystem;

    [SetUp]
    public void Setup()
    {
        tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(tempDirectory);
        fileSystem = new FileSystem();
    }

    [TearDown]
    public void TearDown()
    {
        Directory.Delete(tempDirectory, true);
    }

    [Test]
    public void CreateDirectoryIfNotExists_NotExists_DirectoryCreated()
    {
        var directory = Path.Combine(tempDirectory, "newDirectory");
        fileSystem.CreateDirectoryIfNotExists(directory);
        Assert.That(Directory.Exists(directory));
    }
    [Test]
    public void CreateDirectoryIfNotExists_CallSeveralTimes_DoesNotThrow()
    {
        var directory = Path.Combine(tempDirectory, "newDirectory");
        fileSystem.CreateDirectoryIfNotExists(directory);
        fileSystem.CreateDirectoryIfNotExists(directory);
        Assert.Pass();
    }
    [Test]
    public void DeleteFileIfExists_CreateAndDeleteFile_ExistsAndDeletes()
    {
        var file = Path.Combine(tempDirectory, "fileName");
        using (File.Create(file)) { }
        Assert.That(File.Exists(file));

        fileSystem.DeleteFileIfExists(file);
        Assert.That(!File.Exists(file));
    }
    [Test]
    public void DeleteFileIfExists_DeleteRandomFile_DoesNothing()
    {
        var file = Path.Combine(tempDirectory, "someOtherName");
        
        fileSystem.DeleteFileIfExists(file);
        Assert.Pass();
    }
    [Test]
    public async Task CreateFileAndWriteDataToItAsync__CreatesNewFile()
    {
        var formFile = A.Fake<IFormFile>();
        var file = Path.Combine(tempDirectory, "someOtherName2");
        Assert.That(!File.Exists(file));

        await fileSystem.CreateFileAndWriteDataToItAsync(formFile, file);
        Assert.That(File.Exists(file));
    }
    [Test]
    public async Task CreateFileAndWriteDataToItAsync__WritesFormFileDataToFile()
    {
        var content = "Test data to be written to the file.";
        using var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();

        var tempFilePath = tempDirectory + "tempfile.txt";
        var fakeFormFile = new FormFile(memoryStream, 0, tempFilePath.Length, "", tempFilePath);
        

        await fileSystem.CreateFileAndWriteDataToItAsync(fakeFormFile, tempFilePath);

        var fileContent = await File.ReadAllTextAsync(tempFilePath);
        Assert.AreEqual(content, fileContent);
    }
}