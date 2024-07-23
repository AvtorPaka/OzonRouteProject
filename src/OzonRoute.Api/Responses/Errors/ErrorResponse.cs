using System.Net;

namespace OzonRoute.Api.Responses.Errors;
public record ErrorResponse(
    HttpStatusCode StatusCode,
    string ExceptionMessage,
    string InnerExceptionMessage
) {}