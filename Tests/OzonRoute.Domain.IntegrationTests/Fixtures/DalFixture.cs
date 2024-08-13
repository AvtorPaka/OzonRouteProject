using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Extensions;

namespace OzonRoute.Domain.IntegrationTests.Fixtures;

public sealed class DalFixture
{
    public ICalculationsRepository CalculationsRepository {get;}
    public ICalculationGoodsRepository CalculationGoodsRepository {get;}

    public DalFixture()
    {   
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("/Users/georgedemchenko/Ozon_CS_data/Ozon.Route.Project/secrets/Databases/appsettings.Tests.json", optional: true, reloadOnChange: true)
            .Build();

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(s => 
            {
                s.AddDalInfrastucture(config)
                 .AddDalRepositories();
            }).Build();


        ClearDatabase(host);
        host.MigrateUp();

        var services = host.Services;
        CalculationsRepository = services.GetRequiredService<ICalculationsRepository>();
        CalculationGoodsRepository = services.GetRequiredService<ICalculationGoodsRepository>();
    }

    private static void ClearDatabase(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(20240803);
    }
}