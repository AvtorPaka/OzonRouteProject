using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;
public interface IGoodsRepository
{
    public Task AddOrUpdate(GoodEntity entity, CancellationToken cancellationToken);
    public Task<ICollection<GoodEntity>> GetAllGoods();
    public Task<GoodEntity> Get(int id);
}