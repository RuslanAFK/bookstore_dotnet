using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Services.Test;

public class BookFilesServiceTest
{
    private BookFilesService _bookFilesService;

    private IUnitOfWork unitOfWork;
    private IBaseRepository<BookFile> bookFileRepository;
    private IFileManager fileManager;
    [SetUp]
    public void Setup()
    {
        bookFileRepository = A.Fake<IBaseRepository<BookFile>>();
        unitOfWork = A.Fake<IUnitOfWork>();
        fileManager = A.Fake<IFileManager>();

        _bookFilesService = new BookFilesService(unitOfWork, fileManager, bookFileRepository);
    }

    [Test]
    public void AddAsync_WithNullBookFile_CallsStoreFileAndGetPathAndAddAsyncAndNotDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = null;
        var formFile = A.Fake<IFormFile>();
        async Task Action() => await _bookFilesService.AddAsync(book, formFile);
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => fileManager.StoreFileAndGetPath(formFile)).MustHaveHappenedOnceExactly();
        A.CallTo(() => bookFileRepository.AddAsync(A<BookFile>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustNotHaveHappened();
    }
    [Test]
    public void AddAsync_WithBookFile_NotCallsDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = A.Dummy<BookFile>();
        var formFile = A.Fake<IFormFile>();
        async Task Action() => await _bookFilesService.AddAsync(book, formFile);
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void RemoveAsync_WithNullBookFile_ThrowsEntityNotFoundException()
    {
        var book = A.Dummy<Book>();
        book.BookFile = null;
        var formFile = A.Fake<IFormFile>();
        async Task Action() => await _bookFilesService.RemoveAsync(book);
        Assert.ThrowsAsync<EntityNotFoundException>(Action);
    }
    [Test]
    public void RemoveAsync_WithBookFile_CallsDeleteFileAndRemove()
    {
        var book = A.Dummy<Book>();
        book.BookFile = A.Dummy<BookFile>();
        var formFile = A.Fake<IFormFile>();
        async Task Action() => await _bookFilesService.RemoveAsync(book);
        Assert.DoesNotThrowAsync(Action);
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => bookFileRepository.Remove(A<BookFile>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ThrowIfFileNotPermitted_FileLength0_ThrowsFileEmptyException()
    {
        var baseStream = A.Dummy<Stream>();
        var length = 0;
        var fileName = A.Dummy<string>();
        IFormFile formFile = new FormFile(baseStream, 0, length, string.Empty, fileName);
        void Action() => _bookFilesService.ThrowIfFileNotPermitted(formFile);
        Assert.Throws<FileEmptyException>(Action);
    }
    [Test]
    public void ThrowIfFileNotPermitted_FileNameRandom_ThrowsUnsupportedFileTypeException()
    {
        var baseStream = A.Dummy<Stream>();
        var length = 1;
        var fileName = A.Dummy<string>();
        IFormFile formFile = new FormFile(baseStream, 0, length, string.Empty, fileName);
        void Action() => _bookFilesService.ThrowIfFileNotPermitted(formFile);
        Assert.Throws<UnsupportedFileTypeException>(Action);
    }
    [Test]
    public void ThrowIfFileNotPermitted_LengthMoreThan0AndFileNameEndsWithPdf_AllPasses()
    {
        var baseStream = A.Dummy<Stream>();
        var length = 1;
        var fileName = "askdsjbv.pdf";
        IFormFile formFile = new FormFile(baseStream, 0, length, string.Empty, fileName);
        void Action() => _bookFilesService.ThrowIfFileNotPermitted(formFile);
        Assert.DoesNotThrow(Action);
    }
}