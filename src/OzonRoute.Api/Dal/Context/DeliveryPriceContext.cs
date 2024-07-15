using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Context;

public class DeliveryPriceContext
{
    public List<GoodPriceEntity> Storage { get; init; } = new List<GoodPriceEntity>();

    public ReportEntity Report {get; init;} = new ReportEntity();

    public DeliveryPriceContext()
    {
        Storage = new List<GoodPriceEntity>();
        Report = new ReportEntity();
    }
}