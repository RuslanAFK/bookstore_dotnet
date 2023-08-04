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
    public void CreateDirectoryIfNotExists_CreatedDirectory()
    {
        var newFolder = "newFolder";
        var directory = Path.Combine(tempDirectory, newFolder);
        fileSystem.CreateDirectoryIfNotExists(directory);
        Assert.That(Directory.Exists(directory));
    }
    [Test]
    public void CreateDirectoryIfNotExists_IfReCalled_DoesNotThrow()
    {
        var newFolder = "newFolder";
        var directory = Path.Combine(tempDirectory, newFolder);
        fileSystem.CreateDirectoryIfNotExists(directory);
        fileSystem.CreateDirectoryIfNotExists(directory);
        Assert.Pass();
    }
    [Test]
    public void DeleteFileIfExists_IfExists_Deletes()
    {
        var file = Path.Combine(tempDirectory, "fileName");
        using (File.Create(file)) { }
        fileSystem.DeleteFileIfExists(file);
        Assert.That(File.Exists(file), Is.False);
    }
    [Test]
    public void DeleteFileIfExists_DoesNotExist_DoesNothing()
    {
        var randomName = "someOtherName";
        var file = Path.Combine(tempDirectory, randomName);
        fileSystem.DeleteFileIfExists(file);
        Assert.Pass();
    }
    [Test]
    public async Task CreateFileAndWriteDataToItAsync_CreatesNewFile()
    {
        var formFile = A.Fake<IFormFile>();
        var fileName = "someOtherName";
        var file = Path.Combine(tempDirectory, fileName);
        await fileSystem.CreateFileAndWriteDataToItAsync(formFile, file);
        Assert.That(File.Exists(file), Is.True);
    }
    [Test]
    public async Task CreateFileAndWriteDataToItAsync_WritesFormFileDataToFile()
    {
        var content = "Test data to be written to the file.";
        using var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        var tempFilePath = tempDirectory + "tempfile.txt";
        var fakeFormFile = new FormFile(memoryStream, 0, content.Length, "", tempFilePath);

        await fileSystem.CreateFileAndWriteDataToItAsync(fakeFormFile, tempFilePath);
        var fileContent = await File.ReadAllTextAsync(tempFilePath);
        Assert.That(content, Is.EqualTo(fileContent));
    }
}