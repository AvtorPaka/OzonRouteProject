namespace OzonRoute.Domain.Shared.Data.Entities;

public record CalculationGoodEntityV1
{
    public long Id {get; init;}
    public long UserId{get; init;}
    //In cm
    public double Width {get; init;}
    public double Height {get;init;}
    public double Length {get; init;}
    //In kg
    public double Weight {get; init;}
}