using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Services.Interfaces;
public interface IReportsService
{
    public Task<ReportModel> GetReport(long userId, CancellationToken cancellationToken);
    public Task UpdateReportData(SaveCalculationModel saveModel, CancellationToken cancellationToken);
    public Task ClearReportData(long userId, CancellationToken cancellationToken);
}