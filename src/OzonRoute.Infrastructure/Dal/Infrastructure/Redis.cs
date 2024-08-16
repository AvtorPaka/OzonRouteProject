using Microsoft.Extensions.DependencyInjection;
using OzonRoute.Infrastructure.Dal.Configuration;
using StackExchange.Redis;

namespace OzonRoute.Infrastructure.Dal.Infrastructure;

public static class Redis
{
    public static void AddIDistributedCache(IServiceCollection services, RedisCacheOptions redisOptions)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = GenerateRedisConfOptions(redisOptions, clientName: "asp.net-rdb", allowAdmin: true);
            options.InstanceName = "rdb_";
        });
    }

    public static void AddOutputCache(IServiceCollection services, RedisCacheOptions redisOptions)
    {
        services.AddStackExchangeRedisOutputCache(options =>
        {
            options.ConfigurationOptions = GenerateRedisConfOptions(redisOptions, clientName: "asp.net-outc", allowAdmin: true);
            options.InstanceName = "outc";
        });

        services.AddOutputCache(options =>
        {   
            options.AddPolicy("storage-goods-get", builder => 
            {
                builder.Expire(TimeSpan.FromSeconds(60));
                builder.Tag("tag-storage");
            });

            options.AddPolicy("storage-goods-price", builder => 
            {
                builder.Expire(TimeSpan.FromSeconds(60));
                builder.SetVaryByQuery("Id");
                builder.Tag("tag-storage");
            });
        });
    }

    private static StackExchange.Redis.ConfigurationOptions GenerateRedisConfOptions(RedisCacheOptions redisOptions, string clientName = "asp.net", bool allowAdmin = false)
    {
        var configOptions = ConfigurationOptions.Parse(redisOptions.ConnectionString);
        configOptions.User = redisOptions.User;
        configOptions.Password = redisOptions.Password;
        configOptions.AllowAdmin = allowAdmin;
        configOptions.ClientName = clientName;

        return configOptions;
    }
}