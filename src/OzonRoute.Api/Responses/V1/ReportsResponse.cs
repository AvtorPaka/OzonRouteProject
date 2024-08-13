namespace OzonRoute.Api.Responses.V1;

public record ReportsResponse(
    double MaxWeight,
    double MaxVolume,
    int MaxDistanceForHeaviestGood,
    int MaxDistanceForLargestGood,
    int TotalNumberOfGoods,
    double SummaryPrice,
    double WavgPrice
) {}
