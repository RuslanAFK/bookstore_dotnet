namespace Services.Test;

public class BookServiceTest
{
    private IFileManager fileManager = null!;
    private IUnitOfWork unitOfWork = null!;
    private BooksService booksService = null!;
    [SetUp]
    public void Setup()
    {
        fileManager = A.Fake<IFileManager>();
        unitOfWork = A.Fake<IUnitOfWork>();
        booksService = new BooksService(unitOfWork, fileManager);
    }
    [Test]
    public async Task GetByIdAsync_ReturnsBook()
    {
        var returnValue = await booksService.GetByIdIncludingFilesAsync(0);
        Assert.That(returnValue, Is.InstanceOf<Book>());
    }
    [Test]
    public async Task AddAsync_CallsAddAsyncAndCompleteAsync()
    {
        var book = A.Dummy<Book>();
        await booksService.AddAsync(book);
        A.CallTo(() => unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task RemoveAsync_WithoutBookFile_CallsRemoveAndCompleteAsyncButNotDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = null;
        await booksService.RemoveAsync(book);
        A.CallTo(() => unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustNotHaveHappened();
    }
    [Test]
    public async Task RemoveAsync_WithBookFile_CallsRemoveAndCompleteAsyncAndDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = A.Dummy<BookFile>();
        await booksService.RemoveAsync(book);
        A.CallTo(() => unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustHaveHappenedOnceExactly();
    }
}