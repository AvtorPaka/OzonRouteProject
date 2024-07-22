namespace OzonRoute.Domain.Models;
public record ReportModel(
    double MaxWeight = 0.0d, // In kg
    double MaxVolume = 0.0d, // In m^3
    int MaxDistanceForHeaviestGood = 0, //In m
    int MaxDistanceForLargestGood = 0,
    double WavgPrice = 0.0d
)
{ }