using Microsoft.Extensions.DependencyInjection;
using OzonRoute.Infrastructure.Dal.Configuration;
using StackExchange.Redis;

namespace OzonRoute.Infrastructure.Dal.Infrastructure;

public static class Redis
{
    public static void AddIDistributedCache(IServiceCollection services, RedisCacheOptions redisOptions)
    {
        var configOptions = ConfigurationOptions.Parse(redisOptions.ConnectionString);
        configOptions.User = redisOptions.User;
        configOptions.Password = redisOptions.Password;
        configOptions.AllowAdmin = true;
        configOptions.ClientName = "ASP.net";

        services.AddStackExchangeRedisCache(x =>
        {
            x.ConfigurationOptions = configOptions;
        });
    }
}