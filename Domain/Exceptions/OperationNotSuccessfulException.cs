using System.Net;

namespace Domain.Exceptions;

public class OperationNotSuccessfulException : BaseException
{
    public override string Message { get; }
    public override HttpStatusCode  StatusCode { get; } = HttpStatusCode.BadRequest;

    public OperationNotSuccessfulException(string message = "Updating database was not successful.")
    {
        Message = message;
    }
}