using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Infrastructure;

namespace OzonRoute.Infrastructure.Dal.Repositories;

public class CalculationGoodsRepository : BaseRepository, ICalculationGoodsRepository
{
    public CalculationGoodsRepository([FromKeyedServices(DatabaseType.CalculationsDb)] NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<long[]> Add(CalculationGoodEntityV1[] calculationGoods, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
insert into delivery_goods (user_id, width, height, length, weight)
    select user_id, width, height, length, weight
    from UNNEST(@Goods)
    returning id;
";      
        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQueryParams = new 
        {
            Goods = calculationGoods
        };

        var goodsIds = await connection.QueryAsync<long>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );

        return goodsIds.ToArray();
    }

    public async Task<IReadOnlyList<CalculationGoodEntityV1>> Query(long userId, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
select id, user_id, width, height, length, weight from delivery_goods
    where user_id = @UserId;
";

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQueryParams = new
        {
            UserId = userId
        };

        var goods = await connection.QueryAsync<CalculationGoodEntityV1>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );

        return goods.ToList();
    }
}