using System.Net;

namespace Domain.Exceptions;

public class FileEmptyException : BaseException
{
    public override string Message { get; } = "File cannot be empty.";
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;
}