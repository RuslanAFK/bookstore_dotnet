namespace Data.Test.Exceptions;

public class SameValueAssignExceptionTest
{
    [Test]
    public void Message_BuildsAsExpected()
    {
        var expectedMessage = "Property Role had the same value you are assigning. Operation is not needed.";
        var exception = new SameValueAssignException(nameof(User.Role));
        var actualMessage = exception.Message;
        Assert.That(actualMessage, Is.EqualTo(expectedMessage));
    }
}