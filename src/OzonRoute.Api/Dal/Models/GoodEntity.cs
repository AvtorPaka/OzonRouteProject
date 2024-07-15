namespace OzonRoute.Api.Dal.Models;

public record GoodEntity (
    //In m^3
    double Volume = 0.0d,
    //In kg
    double Weight = 0.0
) {}