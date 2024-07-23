using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Contexts;

namespace OzonRoute.Infrastructure.Dal.Repositories;

internal sealed class GoodsRepository : IGoodsRepository
{
    private readonly GoodsContext _goodsContext;

    public GoodsRepository(GoodsContext goodsContext)
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

    public async Task<GoodEntity> Get(int id)
    {
        return await Task.FromResult(_goodsContext.Store[id]);
    }
}