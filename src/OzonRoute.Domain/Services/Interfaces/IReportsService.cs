using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Services.Interfaces;
public interface IReportsService
{
    public Task<ReportModel> GetReport(CancellationToken cancellationToken);
    public Task CalculateNewReportData(IReadOnlyList<DeliveryGoodModel> goods, int distance, double finalPrice, CancellationToken cancellationToken);
}