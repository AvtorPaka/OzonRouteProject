using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.NameTranslation;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Infrastructure.Dal.Configuration;
using OzonRoute.Infrastructure.Extensions;

namespace OzonRoute.Infrastructure.Dal.Infrastructure;

public static class Postgres
{   
    private static readonly INpgsqlNameTranslator _translator = new NpgsqlSnakeCaseNameTranslator();

    public static void MapCompositeTypes()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public static void AddDataSource(IServiceCollection services, string connectionString)
    {   
        services.AddNpgsqlDataSource(
            connectionString,
            builder => {
                builder.MapComposite<CalculationEntityV1>("calculations_v1", _translator);
                builder.MapComposite<CalculationGoodEntityV1>("calculation_goods_v1", _translator);
            },
            serviceKey: DatabaseType.CalculationsDb);
    }

    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(r => r
            .AddPostgres()
            .WithGlobalConnectionString(s => 
            {
                var options = s.GetConfiguration<PostgreSQLOptions>();
                return options.ConnectionString;
            })
            .ScanIn(typeof(Postgres).Assembly).For.Migrations())
            .AddLogging(x => x.AddFluentMigratorConsole()); //Also duplicates default Microsoft.Logging, annoying
    }
}