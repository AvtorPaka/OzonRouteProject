using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Domain.Shared.Data.Models;
using OzonRoute.Infrastructure.Dal.Infrastructure;

namespace OzonRoute.Infrastructure.Dal.Repositories;

internal sealed class CalculationsRepository : BaseRepository, ICalculationsRepository
{
    public CalculationsRepository([FromKeyedServices(DatabaseType.CalculationsDb)] NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<long[]> Add(CalculationEntityV1[] calculations, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
insert into calculations (user_id, good_ids, total_volume, total_weight, price, distance, at)
    select user_id, good_ids, total_volume, total_weight, price, distance, at
    from UNNEST(@Calculations)
    returning id;
";
        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQueryParams = new
        {
            Calculations = calculations
        };

        var calculationsIds = await connection.QueryAsync<long>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );

        return calculationsIds.ToArray();
    }

    public async Task<IReadOnlyList<CalculationEntityV1>> Query(CalculationHistoryQueryModel model, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
select id, user_id, good_ids, total_volume, total_weight, price, distance, at
from calculations
where user_id = @UserId
order by at desc
limit @Limit offset @Offset;";

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQueryParams = new
        {
            UserId = model.UserID,
            Limit = model.Limit,
            Offset = model.Offset
        };

        var calculations = await connection.QueryAsync<CalculationEntityV1>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );

        return calculations.ToList();
    }

    public void ClearData()
    {
        throw new NotImplementedException();
    }

}
