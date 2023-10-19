namespace Services.Test;

public class FileManagerTest
{
    private IFileSystem fileSystem = null!;
    private FileManager fileManager = null!;
    [SetUp]
    public void SetUp()
    {
        var host = A.Fake<IHostEnvironment>();
        fileSystem = A.Fake<IFileSystem>();
        fileManager = new FileManager(host, fileSystem);
    }

    [Test]
    public async Task StoreFileAndGetPath_ReturnsGuidAndExtension()
    {
        var extension = "ext";
        var fileName = "something" + "." + extension;
        var formFile = DataGenerator.CreateTestFormFile(fileName);
        var newName = await fileManager.StoreFileAndGetPath(formFile);

        var splittedName = newName.Split('.');
        var guidPart = splittedName[0];
        var extensionPart = splittedName[1];
        var parsedGuidPart = Guid.Parse(guidPart);
        Assert.That(parsedGuidPart, Is.InstanceOf<Guid>());
        Assert.That(extensionPart, Is.EqualTo(extension));
    }
    [Test]
    public async Task StoreFileAndGetPath_CallsWriteToFileAndCreateDirectoryIfNotExists()
    {
        var formFile = DataGenerator.CreateTestFormFile();
        await fileManager.StoreFileAndGetPath(formFile);
        A.CallTo(() => fileSystem.CreateFileAndWriteDataToItAsync(A<IFormFile>._, A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileSystem.CreateDirectoryIfNotExists(A<string>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public void DeleteFile_CallsDeleteFileIfExistsAndCreateDirectoryIfNotExists()
    {
        var fileName = A.Dummy<string>();
        fileManager.DeleteFile(fileName);
        A.CallTo(() => fileSystem.DeleteFileIfExists(A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileSystem.CreateDirectoryIfNotExists(A<string>._)).MustHaveHappenedOnceExactly();
    }
}