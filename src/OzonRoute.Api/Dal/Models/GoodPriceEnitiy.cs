namespace OzonRoute.Api.Dal.Models;

public record GoodPriceEntity(
    DateTime At,
    double Volume = 0.0, //In cm^3
    double Weight = 0.0, //In gramms
    double Price = 0.0,
    double Distance = 0.0 //In metrs
) {}