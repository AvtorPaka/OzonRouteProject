using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OzonRoute.Infrastructure.Extensions;

namespace OzonRoute.Domain.IntegrationTests.Fixtures;

public class DalFixture
{
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
    }

    private static void ClearDatabase(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(20240803);
    }
}