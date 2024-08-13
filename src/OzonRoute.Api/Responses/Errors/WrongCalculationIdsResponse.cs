using System.Net;

namespace OzonRoute.Api.Responses.Errors;

public record WrongCalculationIdsResponse
(  
    HttpStatusCode StatusCode,
    IEnumerable<long> WrongCalculationIds,
    IEnumerable<string> Exceptions
) {}