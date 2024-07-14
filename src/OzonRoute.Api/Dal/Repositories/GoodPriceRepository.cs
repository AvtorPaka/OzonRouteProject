using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Dal.Context;
using OzonRoute.Api.Dal.Models;
using OzonRoute.Api.Dal.Repositories.Interfaces;

namespace OzonRoute.Api.Dal.Repositories;

public class GoodPriceRepository : IGoodPriceRepository
{
    private readonly DeliveryPriceContext _deliveryPriceContext;

    public GoodPriceRepository([FromServices] DeliveryPriceContext deliveryPriceContext)
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

    public async Task<List<GoodPriceEntity>> QueryData(CancellationToken cancellationToken)
    {   
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken); // Fiction
        return await Task.FromResult(_deliveryPriceContext.Storage.ToList());
    }
}
