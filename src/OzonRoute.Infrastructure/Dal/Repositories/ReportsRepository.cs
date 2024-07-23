using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Contexts;

namespace OzonRoute.Infrastructure.Dal.Repositories;

public class ReportsRepository : IReportsRepository
{
    private readonly DeliveryPriceContext _deliveryPriceContext;

    public ReportsRepository(DeliveryPriceContext deliveryPriceContext)
    {
        _deliveryPriceContext = deliveryPriceContext;
    }

    public async Task<ReportEntity> GetReportData(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction
        return _deliveryPriceContext.Report;
    }
    public void UpdateWavgPrice(double goodsFinalPrice, int goodsCount)
    {
        _deliveryPriceContext.Report.SummaryPrice += goodsFinalPrice;
        _deliveryPriceContext.Report.TotalNumberOfGoods += goodsCount;
    }
    
    public void UpdateMaxVolumeAndDistance(double maxVolume, int distance)
    {
        _deliveryPriceContext.Report.MaxVolume = maxVolume;
        _deliveryPriceContext.Report.MaxDistanceForLargestGood = distance;
    }

    public void UpdateMaxWeightAndDistance(double maxWeight, int distance)
    {
        _deliveryPriceContext.Report.MaxWeight = maxWeight;
        _deliveryPriceContext.Report.MaxDistanceForHeaviestGood = distance;
    }
}