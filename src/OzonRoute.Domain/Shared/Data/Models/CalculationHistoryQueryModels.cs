namespace OzonRoute.Domain.Shared.Data.Models;

public record CalculationHistoryQueryModel(
    long UserID,
    int Limit,
    int Offset
) {}