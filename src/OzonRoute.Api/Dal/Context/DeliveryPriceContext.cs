using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Context;

public class DeliveryPriceContext
{
    public List<GoodPriceEntity> Storage { get; set; } = new List<GoodPriceEntity>();

    public DeliveryPriceContext()
    {
        Storage = new List<GoodPriceEntity>();
    }
}