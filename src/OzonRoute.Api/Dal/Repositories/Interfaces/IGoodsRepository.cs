using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Repositories.Interfaces;
public interface IGoodsRepository
{
    public Task AddOrUpdate(GoodEntity entity, CancellationToken cancellationToken);
    public Task<ICollection<GoodEntity>> GetAllGoods();
    public Task<GoodEntity> Get(int id);
}