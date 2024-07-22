namespace OzonRoute.Domain.Configuration.Models;

public sealed class PriceCalculatorOptions
{
    public double VolumeToPriceRatio { get; init; }
    public double WeightToPriceRatio { get; init; }
}