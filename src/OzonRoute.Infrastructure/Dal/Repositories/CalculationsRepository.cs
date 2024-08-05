using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Contexts;

namespace OzonRoute.Infrastructure.Dal.Repositories;

internal sealed class CalculationsRepository : ICalculationsRepository
{
    private readonly DeliveryPriceContext _deliveryPriceContext;

    public CalculationsRepository(DeliveryPriceContext deliveryPriceContext)
    {
        _deliveryPriceContext = deliveryPriceContext;
    }

    public void Save(CalculationEntityV1 goodPriceData)
    {
        _deliveryPriceContext.Storage.Add(goodPriceData);
    }

    public void ClearData()
    {
        _deliveryPriceContext.Storage.Clear();
    }

    public async Task<IReadOnlyList<CalculationEntityV1>> QueryData(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken); // Fiction
        return await Task.FromResult(_deliveryPriceContext.Storage.ToList());
    }
}
