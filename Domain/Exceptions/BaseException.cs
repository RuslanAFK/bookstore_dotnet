using System.Net;

namespace Domain.Exceptions;

public abstract class BaseException : Exception
{
    public override string Message { get; } = null!;
    public abstract HttpStatusCode StatusCode { get; }
}