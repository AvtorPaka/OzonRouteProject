using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Contexts;

namespace OzonRoute.Infrastructure.Dal.Repositories;

public class GoodPriceRepository : IGoodPriceRepository
{
    private readonly DeliveryPriceContext _deliveryPriceContext;

    public GoodPriceRepository(DeliveryPriceContext deliveryPriceContext)
    {
        _deliveryPriceContext = deliveryPriceContext;
    }

    public void Save(GoodPriceEntity goodPriceData)
    {
        _deliveryPriceContext.Storage.Add(goodPriceData);
    }

    public void ClearData()
    {
        _deliveryPriceContext.Storage.Clear();
    }

    public async Task<IReadOnlyList<GoodPriceEntity>> QueryData(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken); // Fiction
        return await Task.FromResult(_deliveryPriceContext.Storage.ToList());
    }
}
