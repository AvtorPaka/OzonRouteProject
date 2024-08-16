using OzonRoute.Infrastructure.Dal.Infrastructure;

namespace OzonRoute.Infrastructure.Services.Interfaces;

public interface IOutputCacheEvictService
{
    public Task EvictOutputCache(TagType tagType, CancellationToken cancellationToken);
}