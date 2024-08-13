using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Configuration;
using OzonRoute.Infrastructure.Dal.Extensions;

namespace OzonRoute.Infrastructure.Dal.Repositories;

internal sealed class ReportsRepository : IReportsRepository
{
    private readonly IDistributedCache _redis;

    private readonly ReportsRepositoryCacheExpirationOptions _expirationOptions;

    public ReportsRepository(IOptionsSnapshot<CacheExpirationOptions> options, IDistributedCache cache)
    {
        _redis = cache;
        _expirationOptions = options.Value.ReportsRepositoryCacheExpirationOptions;
    }

    public async Task AddOrUpdate(ReportEntity entity, CancellationToken cancellationToken)
    {
        await _redis.SetObjectAsync<ReportEntity>(
            value: entity,
            token: cancellationToken
        );
    }

    public async Task AddOrUpdateWithExpiration(ReportEntity entity, CancellationToken cancellationToken)
    {   
        var cacheOptions = new DistributedCacheEntryOptions();
        cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(_expirationOptions.AbsoluteExpirationTime));

        await _redis.SetObjectAsync<ReportEntity>(
            value: entity,
            cacheOptions: cacheOptions,
            token: cancellationToken
        );
    }

    public async Task<ReportEntity> Get(long userId, CancellationToken cancellationToken)
    {
        string cacheKey = GenerateCacheKey(userId);

        return await _redis.GetObjectAsync<ReportEntity>(
            key: cacheKey,
            cancellationToken: cancellationToken
        ) ?? new ReportEntity();
    }

    public async Task Delete(long userId, CancellationToken cancellationToken)
    {
        await _redis.RemoveAsync(
            key: GenerateCacheKey(userId),
            token: cancellationToken
        );
    }

    private static string GenerateCacheKey(long userId) => $"rep_{(userId == -1 ? "g" : userId)}";
}