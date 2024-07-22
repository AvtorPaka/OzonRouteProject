
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface IReportsRepository
{
    public Task<ReportEntity> GetReportData(CancellationToken cancellationToken);
    public void CalculateNewMaxWeightAndDistance(IReadOnlyList<GoodEntityReport> goodPriceEntities, int distance);
    public void CalculateNewMaxVolumeAndDistance(IReadOnlyList<GoodEntityReport> goodPriceEntities, int distance);
    public void CalculateWavgPrice(double goodsFinalPrice, int goodsCount);
}