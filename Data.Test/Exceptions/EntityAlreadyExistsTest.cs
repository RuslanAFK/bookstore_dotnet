namespace Data.Test.Exceptions;

public class EntityAlreadyExistsTest
{
    [Theory]
    [TestCase(typeof(User), nameof(User.Id), "30", "User with Id \"30\" already exists. Consider changing it and trying again.")]
    [TestCase(typeof(Book), nameof(Book.Name), null, "Book with current Name already exists. Consider changing it and trying again.")]
    public void Message_BuildsAsExpected
        (Type type, string propertyName, string? propertyValue, string expectedMessage)
    {
        EntityAlreadyExistsException exception = new EntityAlreadyExistsException(type, propertyName, propertyValue);
        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
}