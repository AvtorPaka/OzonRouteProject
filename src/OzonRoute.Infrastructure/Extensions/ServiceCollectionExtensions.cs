using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OzonRoute.Infrastructure.Dal.Configuration;
using OzonRoute.Infrastructure.Dal.Infrastructure;
using Serilog;
using OzonRoute.Infrastructure.Services;
using OzonRoute.Infrastructure.Services.Interfaces;

namespace OzonRoute.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{   
    public static IServiceCollection AddDalInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {   
        var postgreSqlSection = configuration.GetSection($"DalOptions:{nameof(PostgreSQLOptions)}");
        var redisCacheSection = configuration.GetSection($"DalOptions:{nameof(RedisCacheOptions)}");

        var sqlOptions = postgreSqlSection.Get<PostgreSQLOptions>() ?? throw new ArgumentNullException("PostgreSQLOptions is missing.");
        var redisOptions = redisCacheSection.Get<RedisCacheOptions>() ?? throw new ArgumentNullException("RedisCacheOptions is missing");

        services.Configure<PostgreSQLOptions>(postgreSqlSection);
        services.Configure<RedisCacheOptions>(redisCacheSection);
        services.Configure<CacheExpirationOptions>(configuration.GetSection("CacheExpirationOptions"));

        Postgres.AddDataSource(services, sqlOptions);
        Postgres.ConfigureTypeMapOptions();
        Postgres.AddMigrations(services);

        Redis.AddIDistributedCache(services, redisOptions);
        
        return services;
    }

    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICalculationsRepository , CalculationsRepository>();
        services.AddScoped<ICalculationGoodsRepository, CalculationGoodsRepository>();
        services.AddScoped<IStorageGoodsRepository, StorageGoodsRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {   
        var redisCacheSection = configuration.GetSection($"DalOptions:{nameof(RedisCacheOptions)}");
        var redisOptions = redisCacheSection.Get<RedisCacheOptions>() ?? throw new ArgumentNullException("RedisCacheOptions is missing");

        services.AddSerilog();

        Redis.AddOutputCache(services, redisOptions);

        return services;
    }


    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IGoodsDetachedService, GoodsDetachedService>();
        services.AddScoped<IOutputCacheEvictService, OutputCacheEvictService>();

        return services;
    }
    
}