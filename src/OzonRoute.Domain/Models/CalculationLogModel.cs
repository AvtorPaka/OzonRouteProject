namespace OzonRoute.Domain.Models;

public record CalculationLogModel(
    long Id,
    long UserId,
    long[] GoodsIds,
    DateTimeOffset At,
    double TotalVolume = 0.0, //In cm^3
    double TotalWeight = 0.0, //In kg
    double Price = 0.0,
    double Distance = 0.0 // In metrs
)
{ }
