using System.Runtime.Serialization;

namespace Application.Exceptions;

public class AthenaException : Exception
{
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; }

    public AthenaException(SerializationInfo info, StreamingContext context, int statusCode, List<string> errors) :
        base(info, context)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public AthenaException(string? message, int statusCode) :
        base(message)
    {
        StatusCode = statusCode;
        Errors = new List<string>();
    }

    public AthenaException(string? message, int statusCode, List<string> errors)
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public AthenaException(string? message, Exception? innerException, int statusCode, List<string> errors)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}