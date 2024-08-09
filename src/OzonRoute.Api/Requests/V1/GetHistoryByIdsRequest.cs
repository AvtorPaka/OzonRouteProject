namespace OzonRoute.Api.Requests.V1;

public record GetHistoryByIdsRequest (
    long UserId,
    List<long> CalculationIds
) {}