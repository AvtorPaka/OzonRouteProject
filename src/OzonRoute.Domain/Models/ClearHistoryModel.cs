namespace OzonRoute.Domain.Models;

public record ClearHistoryModel(
    long UserId,
    List<long> CalculationIds
)
{ }