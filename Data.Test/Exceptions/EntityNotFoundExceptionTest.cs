namespace Data.Test.Exceptions;

public class EntityNotFoundExceptionTest
{
    [Theory]
    [TestCase(typeof(User), nameof(User.Id), "30", "User with Id \"30\" is not found. Please check if you entered the correct data.")]
    [TestCase(typeof(Book), nameof(Book.Name), null, "Book with current Name is not found. Please check if you entered the correct data.")]
    public void Message_BuildsAsExpected
        (Type type, string propertyName, string? propertyValue, string expectedMessage)
    {
        EntityNotFoundException exception = new EntityNotFoundException(type, propertyName, propertyValue);
        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
}