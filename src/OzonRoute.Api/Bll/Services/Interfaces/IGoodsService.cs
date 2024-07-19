using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Bll.Services.Interfaces;

public interface IGoodsService
{
    public Task<IEnumerable<GoodEntity>> GetGoodsFromDetached(CancellationToken cancellationToken);
    public Task<IReadOnlyList<GoodStoreModel>> GetGoodsFromData(CancellationToken cancellationToken);
    public Task UpdateGoods(IEnumerable<GoodEntity> goodEntities, CancellationToken cancellationToken);
    public Task<GoodEntity> GetGoodFromData(int id);
}