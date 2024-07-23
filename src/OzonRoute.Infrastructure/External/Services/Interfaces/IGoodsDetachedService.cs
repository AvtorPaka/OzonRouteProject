using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.External.Services.Interfaces;

public interface IGoodsDetachedService
{
    public Task<IEnumerable<GoodEntity>> GetGoodsFromDetached(CancellationToken cancellationToken);

}