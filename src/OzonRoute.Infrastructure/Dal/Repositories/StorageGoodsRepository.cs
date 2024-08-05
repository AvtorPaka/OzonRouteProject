using OzonRoute.Domain.Exceptions.Infrastructure;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Contexts;

namespace OzonRoute.Infrastructure.Dal.Repositories;

internal sealed class StorageGoodsRepository : IStorageGoodsRepository
{
    private readonly StorageGoodsContext _storageGoodsContext;

    public StorageGoodsRepository(StorageGoodsContext storageGoodsContext)
    {
        _storageGoodsContext = storageGoodsContext;
    }

    public async Task AddOrUpdate(StorageGoodEntity entity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); //Fiction

        _storageGoodsContext.Store.Remove(entity.Id);
        _storageGoodsContext.Store.Add(entity.Id, entity);
    }

    public async Task<ICollection<StorageGoodEntity>> GetAllGoods()
    {
        return await Task.FromResult(_storageGoodsContext.Store.Select(kv => kv.Value).ToArray());
    }

    public async Task<StorageGoodEntity> Get(int id)
    {   
        try
        {
            return await Task.FromResult(_storageGoodsContext.Store[id]);
        }
        catch (KeyNotFoundException ex)
        {
            throw new EntityNotFoundException("Not found", ex);
        }
    }
}