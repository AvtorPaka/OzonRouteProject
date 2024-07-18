namespace OzonRoute.Api.Dal.Models;

public record GoodEntityReport (
    //In m^3
    double Volume = 0.0d,
    //In kg
    double Weight = 0.0
) {}