namespace OzonRoute.Domain.Models;
public record ReportModel
{
    public ReportModel(long userId, double maxWeight, double maxVolume, int maxDistanceForHeaviestGood, int maxDistanceForLargestGood, int totalNumberOfGoods, double summaryPrice)
    {
        UserId = userId;
        MaxWeight = maxWeight;
        MaxVolume = maxVolume;
        MaxDistanceForHeaviestGood = maxDistanceForHeaviestGood;
        MaxDistanceForLargestGood = maxDistanceForLargestGood;
        TotalNumberOfGoods = totalNumberOfGoods;
        SummaryPrice = summaryPrice;
    }

    public long UserId {get; set;}
    public double MaxWeight {get; set;}
    public double MaxVolume {get; set;}
    public int MaxDistanceForHeaviestGood {get; set;}
    public int MaxDistanceForLargestGood {get; set;}
    public int TotalNumberOfGoods {get; set;}
    public double SummaryPrice {get; set;}

}