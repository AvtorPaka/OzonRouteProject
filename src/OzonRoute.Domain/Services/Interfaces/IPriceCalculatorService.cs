using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Services.Interfaces;

public interface IPriceCalculatorService
{
    public Task<double> CalculatePrice(IReadOnlyList<GoodModel> goods, int distance);
    public Task<IReadOnlyList<CalculateLogModel>> QueryLog(int take, CancellationToken cancellationToken);
    public void ClearLog();
}