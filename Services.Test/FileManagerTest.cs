using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Hosting;

namespace Services.Test
{
    public class FileManagerTest
    {
        private IHostEnvironment host;
        private IFileSystem fileSystem;
        private FileManager fileManager;

        [SetUp]
        public void SetUp()
        {
            host = A.Fake<IHostEnvironment>();
            fileSystem = A.Fake<IFileSystem>();
            fileManager = new FileManager(host, fileSystem);
        }

        [Test]
        public async Task StoreFileAndGetPath__ReturnsGuidAndExtension()
        {
            var fileName = "name.ext";
            var formFile = new FormFile(A.Dummy<Stream>(), 0, 0, "", fileName);
            var newName = await fileManager.StoreFileAndGetPath(formFile);
            var parts = newName.Split('.');
            Assert.That(Guid.Parse(parts[0]), Is.InstanceOf<Guid>());
            Assert.That(parts[1], Is.EqualTo("ext"));
        }
        [Test]
        public async Task StoreFileAndGetPath__CallsWriteToFileAndCreateDirectoryIfNotExists()
        {
            var fileName = "name.ext";
            var formFile = new FormFile(A.Dummy<Stream>(), 0, 0, "", fileName);
            await fileManager.StoreFileAndGetPath(formFile);
            A.CallTo(() => fileSystem.CreateFileAndWriteDataToItAsync(formFile, A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fileSystem.CreateDirectoryIfNotExists(A<string>._)).MustHaveHappenedOnceExactly();
        }
        [Test]
        public async Task DeleteFile__CallsDeleteFileIfExistsAndCreateDirectoryIfNotExists()
        {
            fileManager.DeleteFile(A.Dummy<string>());
            A.CallTo(() => fileSystem.DeleteFileIfExists(A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fileSystem.CreateDirectoryIfNotExists(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
