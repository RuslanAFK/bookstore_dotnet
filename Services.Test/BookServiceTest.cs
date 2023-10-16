namespace Services.Test;

public class BookServiceTest
{
    private IFileManager fileManager;
    private IUnitOfWork unitOfWork;
    private BooksService booksService;
    [SetUp]
    public void Setup()
    {
        fileManager = A.Fake<IFileManager>();
        unitOfWork = A.Fake<IUnitOfWork>();
        booksService = new BooksService(unitOfWork, fileManager);
    }
    [Test]
    public async Task GetQueriedAsync_ReturnsListResponseOfBook()
    {
        var query = A.Dummy<Query>();
        var returnValue = await booksService.GetQueriedAsync(query);
        Assert.That(returnValue, Is.InstanceOf<ListResponse<Book>>());
    }
    [Test]
    public async Task GetByIdAsync_ReturnsBook()
    {
        var returnValue = await booksService.GetByIdAsync(0);
        Assert.That(returnValue, Is.InstanceOf<Book>());
    }
    [Test]
    public async Task AddAsync_CallsAddAsyncAndCompleteAsync()
    {
        var book = A.Dummy<Book>();
        await booksService.AddAsync(book);
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task UpdateAsync_CallsUpdateAndCompleteAsync()
    {
        var id = A.Dummy<int>();
        var book = A.Dummy<Book>();
        await booksService.UpdateAsync(id, book);
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public async Task UpdateAsync_AssignsId()
    {
        var expectedId = 158;
        var book = A.Dummy<Book>();
        await booksService.UpdateAsync(expectedId, book);
        var actualId = book.Id;
        Assert.That(actualId, Is.EqualTo(expectedId));
    }
    [Test]
    public async Task RemoveAsync_WithoutBookFile_CallsRemoveAndCompleteAsyncButNotDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = null;
        await booksService.RemoveAsync(book);
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustNotHaveHappened();
    }
    [Test]
    public async Task RemoveAsync_WithBookFile_CallsRemoveAndCompleteAsyncAndDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = A.Dummy<BookFile>();
        await booksService.RemoveAsync(book);
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustHaveHappenedOnceExactly();
    }
}