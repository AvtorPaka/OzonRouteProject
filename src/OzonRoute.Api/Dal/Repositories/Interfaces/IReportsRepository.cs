using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Repositories.Interfaces;

public interface IReportsRepository
{   
    public Task<ReportEntity> GetReportData(CancellationToken cancellationToken);
    public void CalculateNewMaxWeightAndDistance(List<GoodEntity> goodPriceEntities, int distance);
    public void CalculateNewMaxVolumeAndDistance(List<GoodEntity> goodPriceEntities, int distance);
    public void CalculateWavgPrice(double goodsFinalPrice, int goodsCount);
}