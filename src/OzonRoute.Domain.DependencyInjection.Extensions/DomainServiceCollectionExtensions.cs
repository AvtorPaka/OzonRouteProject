using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Services;
using OzonRoute.Domain.Services.Interfaces;

namespace OzonRoute.Domain.DependencyInjection.Extensions;
public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {   
        services.Configure<PriceCalculatorOptions>(configuration.GetSection(nameof(PriceCalculatorOptions)));

        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();

        services.AddScoped<IStorageGoodsService, StorageGoodsService>();
        services.AddScoped<IReportsService, ReportsService>();
        
        //Dont have globaly enabled ValidateAndThrowAsync so unusable, or i didn't found it
        // services.AddValidatorsFromAssemblyContaining<Validators.GetHistoryModelValidator>();
        // services.AddFluentValidationAutoValidation();

        return services;
    }
}