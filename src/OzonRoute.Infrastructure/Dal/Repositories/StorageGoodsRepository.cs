using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonRoute.Domain.Exceptions.Infrastructure;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Infrastructure;

namespace OzonRoute.Infrastructure.Dal.Repositories;

internal sealed class StorageGoodsRepository: BaseRepository, IStorageGoodsRepository
{
    public StorageGoodsRepository([FromKeyedServices(DatabaseType.CalculationsDb)] NpgsqlDataSource dataSource): base(dataSource)
    {
    }

    public async Task AddOrUpdate(StorageGoodEntity[] entities, CancellationToken cancellationToken)
    {   
        var updatedGoodIds = await Update(entities, cancellationToken);

        StorageGoodEntity[] newEntitiesToAdd = entities.Where(x => !updatedGoodIds.Contains(x.Id)).ToArray();
        await Add(newEntitiesToAdd, cancellationToken);
    }

    private async Task Add(StorageGoodEntity[] entities, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
INSERT INTO storage_goods (id, name, count, length, width, height, weight, price)
    SELECT id, name, count, length, width, height, weight, price
    FROM UNNEST(@Goods);";

        var sqlQueryParams = new
        {
            Goods = entities
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var numOfAffectedRows = await connection.ExecuteAsync(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );
    }

    private async Task<HashSet<long>> Update(StorageGoodEntity[] entities, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
UPDATE storage_goods SET
    count = bulk.count,
    length = bulk.length,
    width = bulk.width,
    height = bulk.height,
    weight = bulk.weight,
    price = bulk.price
FROM UNNEST(@Goods) as bulk
WHERE storage_goods.id = bulk.id
returning storage_goods.id;";

        var sqlQueryParams = new
        {
            Goods = entities
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var updatedGoodIds = await connection.QueryAsync<long>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );

        return updatedGoodIds.ToHashSet();
    }
    
    public async Task<IReadOnlyList<StorageGoodEntity>> Query(CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM storage_goods;
        ";

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var queriedGoods = await connection.QueryAsync<StorageGoodEntity>(
            new CommandDefinition(
                commandText: sqlQuery,
                cancellationToken: cancellationToken
            )
        );

        return queriedGoods.ToList();
    }

    public async Task<StorageGoodEntity> QuerySingle(long goodId, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM storage_goods WHERE id = @Id;";

        var sqlQueryParams = new 
        {
            Id = goodId
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        StorageGoodEntity? queriedEntity = await connection.QueryFirstOrDefaultAsync<StorageGoodEntity>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        ) ?? throw new EntityNotFoundException("Storage good not found.");

        return queriedEntity;
    }
}