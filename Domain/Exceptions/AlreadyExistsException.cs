using System.Net;

namespace Domain.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public override HttpStatusCode StatusCode { get; }
    }
}
