using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.Services;

internal sealed class ReportsService : IReportsService
{   
    private readonly IReportsRepository _reportsRepository;

    public ReportsService(IReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
    }
    
    public async Task<ReportModel> GetReport(CancellationToken cancellationToken)
    {
        ReportEntity reportEntity = await _reportsRepository.GetReportData(cancellationToken);
        ReportModel reportModel = await reportEntity.MapEntityToModel();

        return reportModel;
    }

    public async Task CalculateNewReportData(IReadOnlyList<DeliveryGoodModel> goods, int distance, double finalPrice, CancellationToken cancellationToken)
    {   
        await CalculateNewMaxVolumeAndDistance(goods, distance, cancellationToken);
        await CalculateNewMaxWeightAndDistance(goods, distance, cancellationToken);
        _reportsRepository.UpdateWavgPrice(finalPrice, goods.Count);
    }

    private async Task CalculateNewMaxWeightAndDistance(IReadOnlyCollection<DeliveryGoodModel> goods, int distance, CancellationToken cancellationToken)
    {   
        ReportEntity currentReport = await _reportsRepository.GetReportData(cancellationToken);

        double maxCurrentWeight = goods.Max(e => e.Weight);
        if (maxCurrentWeight > currentReport.MaxWeight)
        {
            _reportsRepository.UpdateMaxWeightAndDistance(maxCurrentWeight, distance);
        }
    }

    private async Task CalculateNewMaxVolumeAndDistance(IReadOnlyCollection<DeliveryGoodModel> goods, int distance, CancellationToken cancellationToken)
    {   
        ReportEntity currentReport = await _reportsRepository.GetReportData(cancellationToken);

        double maxCurrentVolume = goods.Max(e => e.CalculateVolume());
        if (maxCurrentVolume > currentReport.MaxVolume)
        {
            _reportsRepository.UpdateMaxVolumeAndDistance(maxCurrentVolume, distance);
        }
    }
}