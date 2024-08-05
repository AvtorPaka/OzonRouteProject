using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;
public interface IStorageGoodsRepository
{
    public Task AddOrUpdate(StorageGoodEntity entity, CancellationToken cancellationToken);
    public Task<ICollection<StorageGoodEntity>> GetAllGoods();
    public Task<StorageGoodEntity> Get(int id);
}