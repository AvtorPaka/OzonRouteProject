namespace OzonRoute.Api.Bll.Models;
public record ReportModel(
    double MaxWeight = 0.0d,
    double MaxVolume = 0.0d,
    int MaxDistanceForHeaviestGood = 0,
    int MaxDistanceForLargestGood = 0,
    double WavgPrice = 0.0d
)
{ }