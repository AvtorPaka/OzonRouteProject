namespace OzonRoute.Api.Bll.Models;

public record CalculateLogModel(
    DateTime At,
    double Volume = 0.0,
    double Price = 0.0
) {}
