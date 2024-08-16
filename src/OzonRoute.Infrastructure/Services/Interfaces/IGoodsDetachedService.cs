using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Services.Interfaces;

public interface IGoodsDetachedService
{
    public Task<IEnumerable<StorageGoodEntity>> GetGoodsFromDetached(CancellationToken cancellationToken);

}