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