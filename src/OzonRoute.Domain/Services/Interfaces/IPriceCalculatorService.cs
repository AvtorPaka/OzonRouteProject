using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Services.Interfaces;

public interface IPriceCalculatorService
{
    public Task<double> CalculatePrice(GoodModelsContainer goodModelsContainer, CancellationToken cancellationToken);
    public Task<IReadOnlyList<CalculateLogModel>> QueryLog(GetHistoryModel model, CancellationToken cancellationToken);
    public void ClearLog();
}