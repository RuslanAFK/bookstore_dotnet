namespace Services.Test;

public class BookFilesServiceTest
{
    private BookFilesService bookFilesService = null!;
    private IUnitOfWork unitOfWork = null!;
    private IFileManager fileManager = null!;
    [SetUp]
    public void Setup()
    {
        unitOfWork = A.Fake<IUnitOfWork>();
        fileManager = A.Fake<IFileManager>();
        bookFilesService = new BookFilesService(unitOfWork, fileManager);
    }
    [Test]
    public async Task AddAsync_WithoutBookFile_Throws3MethodsAndSkips1()
    {
        var book = A.Dummy<Book>();
        book.BookFile = null;
        var formFile = A.Fake<IFormFile>();
        await bookFilesService.AddAsync(book, formFile);
        A.CallTo(() => fileManager.StoreFileAndGetPath(A<IFormFile>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustNotHaveHappened();
    }
    [Test]
    public async Task AddAsync_WithBookFile_Calls4Methods()
    {
        var book = A.Dummy<Book>();
        book.BookFile = A.Dummy<BookFile>();
        var formFile = A.Fake<IFormFile>();
        await bookFilesService.AddAsync(book, formFile);
        A.CallTo(() => fileManager.StoreFileAndGetPath(A<IFormFile>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public void RemoveAsync_WithoutBookFile_ThrowsEntityNotFoundException()
    {
        var book = A.Dummy<Book>();
        book.BookFile = null;
        Assert.ThrowsAsync<EntityNotFoundException>(async() =>
        {
            await bookFilesService.RemoveAsync(book);
        });
    }
    [Test]
    public async Task RemoveAsync_WithBookFile_CallsDeleteFileAndRemove()
    {
        var book = A.Dummy<Book>();
        book.BookFile = A.Dummy<BookFile>();
        await bookFilesService.RemoveAsync(book);
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustHaveHappenedOnceExactly();
    }
    [Test]
    public void ThrowIfFileNotPermitted_FileLength0_ThrowsFileEmptyException()
    {
        var formFile = DataGenerator.CreateTestFormFile(length: 0);
        Assert.Throws<FileEmptyException>(() =>
            bookFilesService.ThrowIfFileNotPermitted(formFile));
    }
    [Test]
    public void ThrowIfFileNotPermitted_FileTypeIncorrect_ThrowsUnsupportedFileTypeException()
    {
        var length = 1;
        var fileName = A.Dummy<string>();
        IFormFile formFile = DataGenerator.CreateTestFormFile(fileName, length);
        Assert.Throws<UnsupportedFileTypeException>(() => 
            bookFilesService.ThrowIfFileNotPermitted(formFile));
    }
    [Test]
    public void ThrowIfFileNotPermitted_LengthMoreThan0AndFileNameEndsWithPdf_Passes()
    {
        var length = 1;
        var fileName = "someFile.pdf";
        IFormFile formFile = DataGenerator.CreateTestFormFile(fileName, length);
        bookFilesService.ThrowIfFileNotPermitted(formFile);
        Assert.Pass();
    }
}