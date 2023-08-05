using System.Net;

namespace Domain.Exceptions;

public class SameValueAssignException : BaseException
{
    public override string Message { get; }
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;
    public string PropertyName { get; }

    public SameValueAssignException(string propertyName)
    {
        PropertyName = propertyName;
        Message = $"Property {PropertyName} had the same value you are assigning. Operation is not needed.";
    }
}