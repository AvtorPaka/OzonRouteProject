using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Detached.Services.Interfaces;

public interface IGoodsDetachedService
{
    public Task<IEnumerable<GoodEntity>> GetGoodsFromDetached(CancellationToken cancellationToken);

}