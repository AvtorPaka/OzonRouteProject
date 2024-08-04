using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.NameTranslation;
using OzonRoute.Infrastructure.Dal.Configuration;

namespace OzonRoute.Infrastructure.Dal.Infrastructure;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator _translator = new NpgsqlSnakeCaseNameTranslator();

    public static void MapCompositeTypes()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(r => r.AddPostgres()
            .WithGlobalConnectionString(s => {
                var postgreCfg = s.GetRequiredService<IOptions<PostgreSQLOptions>>() ?? throw new ArgumentNullException("PostgreSQLOptions configuration is missing.");
                return postgreCfg.Value.ConnectionString;
            })
            .ScanIn(typeof(Postgres).Assembly).For.Migrations())
            .AddLogging(x => x.AddFluentMigratorConsole());
    }
}