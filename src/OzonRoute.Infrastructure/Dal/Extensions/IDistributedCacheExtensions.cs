using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Dal.Extensions;

public static class IDistributedCacheExtensions
{
    private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
    {
        IgnoreReadOnlyProperties = true,
        WriteIndented = false
    };

    public static async Task<T?> GetObjectAsync<T>(this IDistributedCache cache, string key, CancellationToken cancellationToken)
    where T : ICachedEntity
    {
        string? rawJsonValue = await cache.GetStringAsync(
            key: key,
            token: cancellationToken
        );
        
        if (string.IsNullOrEmpty(rawJsonValue))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(rawJsonValue, jsonOptions);
    }

    public static async Task SetObjectAsync<T>(this IDistributedCache cache, T value, CancellationToken token)
    where T: ICachedEntity
    {
        string rawJsonValue = JsonSerializer.Serialize<T>(value);

        await cache.SetStringAsync(
            key: value.CacheKey,
            value: rawJsonValue,
            token: token  
        );
    }

    public static async Task SetObjectAsync<T>(this IDistributedCache cache, T value, DistributedCacheEntryOptions cacheOptions, CancellationToken token)
    where T: ICachedEntity
    {
        string rawJsonValue = JsonSerializer.Serialize<T>(value);

        await cache.SetStringAsync(
            key: value.CacheKey,
            value: rawJsonValue,
            options: cacheOptions,
            token: token  
        );
    }
}
