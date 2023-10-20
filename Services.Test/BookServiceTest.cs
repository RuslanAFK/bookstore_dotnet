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
    public async Task AddAsync_CallsAddAsyncAndCompleteAsync()
    {
        var book = A.Dummy<Book>();
        await booksService.AddAsync(book);
        A.CallTo(() => unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
    }
}