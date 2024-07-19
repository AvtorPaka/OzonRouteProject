using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Dal.Context;
using OzonRoute.Api.Dal.Models;
using OzonRoute.Api.Dal.Repositories.Interfaces;

namespace OzonRoute.Api.Dal.Repositories;

public class GoodsRepository : IGoodsRepository
{   
    private readonly GoodsContext _goodsContext;

    public GoodsRepository([FromServices] GoodsContext goodsContext)
    {
        _goodsContext = goodsContext;
    }

    public async Task AddOrUpdate(GoodEntity entity, CancellationToken cancellationToken)
    {
       await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); //Fiction

        _goodsContext.Store.Remove(entity.Id);
        _goodsContext.Store.Add(entity.Id, entity);
    }

    public async Task<ICollection<GoodEntity>> GetAllGoods()
    {
        return await Task.FromResult(_goodsContext.Store.Select(kv => kv.Value).ToArray());
    }
}