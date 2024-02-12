using Hellang.Middleware.ProblemDetails;

namespace API.Middleware;

public class ExtendedProblemDetail : StatusCodeProblemDetails
{
    public ExtendedProblemDetail(int statusCode) : base(statusCode)
    {
    }

    public List<string>? Errors { get; set; }
}