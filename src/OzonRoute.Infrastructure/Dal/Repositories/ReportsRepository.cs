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

    public void CalculateNewMaxVolumeAndDistance(IReadOnlyList<GoodEntityReport> goodPriceEntities, int distance)
    {
        double maxCurrentVolume = goodPriceEntities.Max(e => e.Volume);
        if (maxCurrentVolume > _deliveryPriceContext.Report.MaxVolume)
        {
            _deliveryPriceContext.Report.MaxVolume = maxCurrentVolume;
            _deliveryPriceContext.Report.MaxDistanceForLargestGood = distance;
        }
    }

    public void CalculateNewMaxWeightAndDistance(IReadOnlyList<GoodEntityReport> goodPriceEntities, int distance)
    {
        double maxCurrentWeight = goodPriceEntities.Max(e => e.Weight);
        if (maxCurrentWeight > _deliveryPriceContext.Report.MaxWeight)
        {
            _deliveryPriceContext.Report.MaxWeight = maxCurrentWeight;
            _deliveryPriceContext.Report.MaxDistanceForHeaviestGood = distance;
        }
    }

    public void CalculateWavgPrice(double goodsFinalPrice, int goodsCount)
    {
        _deliveryPriceContext.Report.SummaryPrice += goodsFinalPrice;
        _deliveryPriceContext.Report.TotalNumberOfGoods += goodsCount;
    }

    public async Task<ReportEntity> GetReportData(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction
        return _deliveryPriceContext.Report;
    }
}