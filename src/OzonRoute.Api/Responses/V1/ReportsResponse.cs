namespace OzonRoute.Api.Responses.V1;

public record ReportsResponse(
    double MaxWeight = 0.0d,
    double MaxVolume = 0.0d,
    int MaxDistanceForHeaviestGood = 0,
    int MaxDistanceForLargestGood = 0,
    double WavgPrice = 0.0d
) {}
