using Microsoft.AspNetCore.OutputCaching;
using OzonRoute.Infrastructure.Dal.Infrastructure;
using OzonRoute.Infrastructure.Services.Interfaces;

namespace OzonRoute.Infrastructure.Services;

public class OutputCacheEvictService : IOutputCacheEvictService
{   
    private readonly IOutputCacheStore _redisOutputCacheStore;

    public OutputCacheEvictService(IOutputCacheStore outputCacheStore)
    {
        _redisOutputCacheStore = outputCacheStore;
    }

    public async Task EvictOutputCache(TagType tagType, CancellationToken cancellationToken)
    {
        await _redisOutputCacheStore.EvictByTagAsync(tagType.Use(), cancellationToken);
    }
}