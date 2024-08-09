using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Services.Interfaces;

public interface IPriceCalculatorService
{
    public Task<double> CalculatePrice(DeliveryGoodsContainer deliveryGoodsContainer, CancellationToken cancellationToken);
    public Task<IReadOnlyList<CalculationLogModel>> QueryLog(GetHistoryModel model, CancellationToken cancellationToken);
    public Task<IReadOnlyList<CalculationLogModel>> QueryLogByIds(GetHistoryByIdsModel model, CancellationToken cancellationToken);
    public Task ClearHistoryLog(ClearHistoryModel model, CancellationToken cancellationToken);
}