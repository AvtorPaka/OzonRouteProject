namespace OzonRoute.Domain.Shared.Data.Entities;

public record GoodEntityReport(
    //In m^3
    double Volume = 0.0d,
    //In kg
    double Weight = 0.0
)
{ }