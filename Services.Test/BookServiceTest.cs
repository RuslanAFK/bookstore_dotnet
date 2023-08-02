namespace Services.Test;

public class BookServiceTest
{
    private IBooksRepository booksRepository;
    private IFileManager fileManager;
    private IUnitOfWork unitOfWork;

    private BooksService _booksService;

    [SetUp]
    public void Setup()
    {
        booksRepository = A.Fake<IBooksRepository>();
        fileManager = A.Fake<IFileManager>();
        unitOfWork = A.Fake<IUnitOfWork>();

        _booksService = new BooksService(booksRepository, unitOfWork, fileManager);
    }

    [Test]
    public async Task GetBooksAsync__ReturnsListResponseOfBook()
    {
        var query = A.Dummy<Query>();
        var returnValue = await _booksService.GetBooksAsync(query);
        Assert.That(returnValue, Is.InstanceOf<ListResponse<Book>>());
    }
    [Test]
    public async Task GetByIdAsync__ReturnsBook()
    {
        var returnValue = await _booksService.GetByIdAsync(0);
        Assert.That(returnValue, Is.InstanceOf<Book>());
    }
    [Test]
    public void AddAsync__CallsAddAsyncAndCompleteAsync()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await _booksService.AddAsync(A.Dummy<Book>());
        });
        A.CallTo(() => booksRepository.AddAsync(A<Book>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public void UpdateAsync__CallsUpdateAndCompleteAsync()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await _booksService.UpdateAsync(0, A.Dummy<Book>());
        });
        A.CallTo(() => booksRepository.Update(A<Book>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
    }
    [Test]
    public void UpdateAsync__ChangesIdValue()
    {
        var id = 158;
        var book = new Book { Id = 0 };
        Assert.DoesNotThrowAsync(async () =>
        {
            await _booksService.UpdateAsync(id, book);
        });
        Assert.That(book.Id, Is.EqualTo(id));
    }

    [Test]
    public void RemoveAsync_WithoutBookFile_CallsRemoveAndCompleteAsyncButNotDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = null;
        Assert.DoesNotThrowAsync(async () =>
        {
            await _booksService.RemoveAsync(book);
        });
        A.CallTo(() => booksRepository.Remove(A<Book>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustNotHaveHappened();
    }
    [Test]
    public void RemoveAsync_WithBookFile_CallsRemoveAndCompleteAsyncAndDeleteFile()
    {
        var book = A.Dummy<Book>();
        book.BookFile = A.Dummy<BookFile>();
        Assert.DoesNotThrowAsync(async () =>
        {
            await _booksService.RemoveAsync(book);
        });
        A.CallTo(() => booksRepository.Remove(A<Book>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.CompleteOrThrowAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => fileManager.DeleteFile(A<string>._)).MustHaveHappenedOnceExactly();
    }
}