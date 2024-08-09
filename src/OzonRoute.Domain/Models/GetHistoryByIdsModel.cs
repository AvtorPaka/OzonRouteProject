namespace OzonRoute.Domain.Models;

public record GetHistoryByIdsModel (
    long UserId,
    List<long> CalculationIds
) {}