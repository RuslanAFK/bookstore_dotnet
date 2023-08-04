using System.Net;

namespace Domain.Exceptions;

public class EntityAlreadyExistsException : BaseException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;
    public override string Message { get; }
    public string Entity { get; }
    public string PropertyName { get; }
    public string? PropertyValue { get; }
    public EntityAlreadyExistsException(Type entityType, string propertyName, string propertyValue = null)
    {
        Entity = entityType.Name;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
        Message = GenerateMessage();
    }
    private string GenerateMessage()
    {
        if (PropertyValue is null)
            return $"{PropertyName} of current {Entity} is already found.";
        return @$"{Entity} with {PropertyName} ""{PropertyValue}"" is already found.";
    }
}