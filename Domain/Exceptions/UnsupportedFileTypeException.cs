using System.Net;

namespace Domain.Exceptions
{
    public class UnsupportedFileTypeException : BaseException
    {
        public override string Message { get; }
        public override HttpStatusCode StatusCode { get; } = HttpStatusCode.UnsupportedMediaType;
        public string[] SupportedFileTypes { get; set; }

        public UnsupportedFileTypeException(params string[] supportedFileTypes)
        {
            SupportedFileTypes = supportedFileTypes;
            Message = GenerateMessage();

        }

        private string GenerateMessage()
        {
            if (SupportedFileTypes.Length == 0)
                return $"Provided unsupported file type.";

            if (SupportedFileTypes.Length == 1)
                return $"Provided unsupported file type. Supported is {SupportedFileTypes[0]}.";

            var supportedStringified = string.Join(',', SupportedFileTypes);
            return $"Provided unsupported file type. Supported are {supportedStringified}.";
        }
    }
}
