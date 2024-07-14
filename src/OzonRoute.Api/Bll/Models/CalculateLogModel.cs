namespace OzonRoute.Api.Bll.Models;

public record CalculateLogModel(
    DateTime At,
    double Volume = 0.0, //In mm^3
    double Weight = 0.0, //In kg
    double Price = 0.0,
    double Distance = 0.0 // In metrs
) {}
