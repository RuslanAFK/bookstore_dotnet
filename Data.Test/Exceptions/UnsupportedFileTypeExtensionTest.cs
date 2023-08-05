namespace Data.Test.Exceptions;

public class UnsupportedFileTypeExtensionTest
{
    [Theory]
    [TestCase("Provided unsupported file type.")]
    [TestCase("Provided unsupported file type. Supported is .pdf.", ".pdf")]
    [TestCase("Provided unsupported file type. Supported are .pdf, .doc.", ".pdf", ".doc")]
    public void Message_BuildsCorrect(string expectedMessage, params string[] supportedFileTypes)
    {
        var exception = new UnsupportedFileTypeException(supportedFileTypes);
        var actualMessage = exception.Message;
        Assert.That(actualMessage, Is.EqualTo(expectedMessage));
    }

}