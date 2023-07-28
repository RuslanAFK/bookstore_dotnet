using System.Net;

namespace Domain.Exceptions;

public class OperationNotSuccessfulException : BaseException
{
    public override string Message { get; }
    public string Function { get; }
    public override HttpStatusCode  StatusCode { get; } = HttpStatusCode.BadRequest;

    public OperationNotSuccessfulException(string function)
    {
        Function = function;
        Message = $"{Function} was not successful.";
    }
}