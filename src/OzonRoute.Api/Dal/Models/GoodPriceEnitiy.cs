namespace OzonRoute.Api.Dal.Models;

public record GoodPriceEntity(
    DateTime At,
    double Volume = 0.0,
    double Price = 0.0
) {}