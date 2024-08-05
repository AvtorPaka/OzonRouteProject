namespace OzonRoute.Domain.Models;

public record CalculationLogModel(
    DateTime At,
    double Volume = 0.0, //In cm^3
    double Weight = 0.0, //In kg
    double Price = 0.0,
    double Distance = 0.0 // In metrs
)
{ }
