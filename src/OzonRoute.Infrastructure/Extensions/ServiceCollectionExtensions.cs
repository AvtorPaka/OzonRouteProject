using OzonRoute.Infrastructure.Dal.Contexts;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;
using OzonRoute.Infrastructure.External.Services;
using OzonRoute.Infrastructure.External.Services.Interfaces;

namespace OzonRoute.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DeliveryPriceContext>();
        services.AddSingleton<GoodsContext>();

        services.AddScoped<IGoodPriceRepository, GoodPriceRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();
        services.AddScoped<IGoodsRepository, GoodsRepository>();

        services.AddScoped<IGoodsDetachedService, GoodsDetachedService>();

        return services;
    }
}