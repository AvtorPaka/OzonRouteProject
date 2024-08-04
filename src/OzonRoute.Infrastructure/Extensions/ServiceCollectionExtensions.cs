using OzonRoute.Infrastructure.Dal.Contexts;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;
using OzonRoute.Infrastructure.External.Services;
using OzonRoute.Infrastructure.External.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using OzonRoute.Infrastructure.Dal.Configuration;
using OzonRoute.Infrastructure.Dal.Infrastructure;

namespace OzonRoute.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{   
    public static IServiceCollection AddDalInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DeliveryPriceContext>();
        services.AddSingleton<GoodsContext>();

        services.Configure<PostgreSQLOptions>(configuration.GetSection($"DalOptions:{nameof(PostgreSQLOptions)}"));

        Postgres.MapCompositeTypes();
        Postgres.AddMigrations(services);
        
        return services;
    }
    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGoodPriceRepository, GoodPriceRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();
        services.AddScoped<IGoodsRepository, GoodsRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IGoodsDetachedService, GoodsDetachedService>();

        return services;
    }
    
}