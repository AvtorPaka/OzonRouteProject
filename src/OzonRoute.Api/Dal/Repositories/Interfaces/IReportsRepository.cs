using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Repositories.Interfaces;

public interface IReportsRepository
{   
    public Task<ReportEntity> GetReportData(CancellationToken cancellationToken);
    public void CalculateNewMaxWeightAndDistance(IReadOnlyList<GoodEntityReport> goodPriceEntities, int distance);
    public void CalculateNewMaxVolumeAndDistance(IReadOnlyList<GoodEntityReport> goodPriceEntities, int distance);
    public void CalculateWavgPrice(double goodsFinalPrice, int goodsCount);
}