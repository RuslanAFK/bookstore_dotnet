using System.Net;

namespace Domain.Exceptions;

public class EntityNotFoundException : BaseException
{
    public string Entity { get; }
    public string PropertyName { get; }
    public string? PropertyValue { get; }
    public override string Message { get; }
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

    public EntityNotFoundException(Type entityType, string propertyName, string? propertyValue=null)
    {
        Entity = entityType.Name;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
        Message = GenerateMessage();
    }
    private string GenerateMessage()
    {
        if (PropertyValue == null)
            return $"{PropertyName} of current {Entity} is not found.";
        return @$"{Entity} with {PropertyName} ""{PropertyValue}"" is not found.";
    }
}