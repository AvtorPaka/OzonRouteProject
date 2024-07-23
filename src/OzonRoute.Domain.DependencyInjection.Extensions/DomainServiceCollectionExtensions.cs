using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Services;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.DependencyInjection.Extensions;
public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {   
        services.Configure<PriceCalculatorOptions>(configuration.GetSection("PriceCalculatorOptions"));

        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>(x => new PriceCalculatorService(
            options: x.GetConfigurationSnapshot<PriceCalculatorOptions>(),
            goodPriceRepository: x.GetRequiredService<IGoodPriceRepository>()
        ));

        services.AddScoped<IGoodsService, GoodsService>();
        services.AddScoped<IReportsService, ReportsService>();

        return services;
    }
}