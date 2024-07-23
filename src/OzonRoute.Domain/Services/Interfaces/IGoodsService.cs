using OzonRoute.Domain.Models;
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Services.Interfaces;

public interface IGoodsService
{
    public Task<IReadOnlyList<GoodStoreModel>> GetGoodsFromData(CancellationToken cancellationToken);
    public Task UpdateGoods(IEnumerable<GoodEntity> goodEntities, CancellationToken cancellationToken);
    public Task<GoodEntity> GetGoodFromData(int id);
}