namespace OzonRoute.Domain.Shared.Data.Entities;

public record CalculationGoodEntityV1(
    long Id,
    long UserId,
    //In cm
    double Width,
    double Height,
    double Length,
    //In kg
    double Weight
) {}