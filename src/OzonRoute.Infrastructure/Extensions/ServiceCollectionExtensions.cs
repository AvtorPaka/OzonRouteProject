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
        services.AddSingleton<StorageGoodsContext>();

        var postgreSqlSection = configuration.GetSection($"DalOptions:{nameof(PostgreSQLOptions)}");
        services.Configure<PostgreSQLOptions>(postgreSqlSection);

        Postgres.AddDataSource(services, postgreSqlSection.GetValue<string>("ConnectionString") ?? throw new ArgumentNullException("PgSQL connection string is missing"));
        Postgres.MapCompositeTypes();
        Postgres.AddMigrations(services);
        
        return services;
    }
    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReportsRepository, ReportsRepository>();
        services.AddScoped<IStorageGoodsRepository, StorageGoodsRepository>();

        services.AddScoped<ICalculationsRepository , CalculationsRepository>();
        services.AddScoped<ICalculationGoodsRepository, CalculationGoodsRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IGoodsDetachedService, GoodsDetachedService>();

        return services;
    }
    
}