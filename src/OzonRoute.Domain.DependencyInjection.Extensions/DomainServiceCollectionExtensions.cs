using FluentValidation;
using FluentValidation.AspNetCore;
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
        services.Configure<PriceCalculatorOptions>(configuration.GetSection(nameof(PriceCalculatorOptions)));

        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>(x => new PriceCalculatorService(
            options: x.GetConfigurationSnapshot<PriceCalculatorOptions>(),
            calculationsRepository: x.GetRequiredService<ICalculationsRepository>(),
            calculationGoodsRepository: x.GetRequiredService<ICalculationGoodsRepository>()
        ));

        services.AddScoped<IStorageGoodsService, StorageGoodsService>();
        services.AddScoped<IReportsService, ReportsService>();
        
        //Dont have globaly enabled ValidateAndThrowAsync so unusable, or i didn't found it
        // services.AddValidatorsFromAssemblyContaining<Validators.GetHistoryModelValidator>();
        // services.AddFluentValidationAutoValidation();

        return services;
    }
}