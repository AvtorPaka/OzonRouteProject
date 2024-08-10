using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface IStorageGoodsRepository: IDbRepository
{
    public Task AddOrUpdate(StorageGoodEntity[] entities, CancellationToken cancellationToken);
    public Task<IReadOnlyList<StorageGoodEntity>> Query(CancellationToken cancellationToken);
    public Task<StorageGoodEntity> QuerySingle(long goodId, CancellationToken cancellationToken);
}