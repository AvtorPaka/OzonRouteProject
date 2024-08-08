using System.Net;

namespace OzonRoute.Api.Responses.Errors;

public record ClearHistoryForbiddenResponse
(  
    HttpStatusCode StatusCode,
    IEnumerable<long> WrongCalculationIds,
    IEnumerable<string> Exceptions
) {}