
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface IReportsRepository
{
    public Task<ReportEntity> GetReportData(CancellationToken cancellationToken);
    public void UpdateWavgPrice(double goodsFinalPrice, int goodsCount);
    public void UpdateMaxVolumeAndDistance(double maxVolume, int distance);
    public void UpdateMaxWeightAndDistance(double maxWeight, int distance);
}