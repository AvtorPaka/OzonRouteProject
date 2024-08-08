namespace OzonRoute.Api.Requests.V1;

public record ClearHistoryRequest (
    long UserId,
    List<long> CalculationIds
) {}