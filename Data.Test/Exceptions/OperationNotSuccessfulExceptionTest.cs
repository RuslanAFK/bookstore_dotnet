namespace Data.Test.Exceptions;

public class OperationNotSuccessfulExceptionTest
{
    [Test]
    public void Message_WithCtorArgument_BuildsAsExpected()
    {
        var inputMessage = "Updating database is not successful.";
        var expectedMessage = inputMessage + " Please retry it later.";
        var exception = new OperationNotSuccessfulException(inputMessage);
        var actualMessage = exception.Message;
        Assert.That(actualMessage, Is.EqualTo(expectedMessage));
    }
    [Test]
    public void Message_WithoutCtorArgument_BuildsAsExpected()
    {
        var expectedMessage = "Operation is not successful. Please retry it later.";
        var exception = new OperationNotSuccessfulException();
        var actualMessage = exception.Message;
        Assert.That(actualMessage, Is.EqualTo(expectedMessage));
    }
}