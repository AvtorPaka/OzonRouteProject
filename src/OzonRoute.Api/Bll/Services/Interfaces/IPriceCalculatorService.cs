using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    public Task<double> CalculatePrice(List<GoodModel> goods, int distance);
    public Task<List<CalculateLogModel>> QueryLog(int take, CancellationToken cancellationToken);
    public void ClearLog();
    public Task<ReportModel> GetReport(CancellationToken cancellationToken);
}