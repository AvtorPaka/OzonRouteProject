namespace OzonRoute.Api.Requests.V2;

public record GetHistoryByIdsRequest (
    long UserId,
    List<long> CalculationIds
) {}