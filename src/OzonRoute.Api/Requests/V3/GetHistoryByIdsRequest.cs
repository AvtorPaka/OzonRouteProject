namespace OzonRoute.Api.Requests.V3;

public record GetHistoryByIdsRequest (
    long UserId,
    List<long> CalculationIds
) {}