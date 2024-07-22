namespace OzonRoute.Domain.Shared.Data.Entities;

public class ReportEntity
{
    public double MaxWeight { get; set; } = 0.0d;
    public double MaxVolume { get; set; } = 0.0d;
    public int MaxDistanceForHeaviestGood { get; set; } = 0;
    public int MaxDistanceForLargestGood { get; set; } = 0;
    public double SummaryPrice { get; set; } = 0.0d;
    public int TotalNumberOfGoods { get; set; } = 0;
}