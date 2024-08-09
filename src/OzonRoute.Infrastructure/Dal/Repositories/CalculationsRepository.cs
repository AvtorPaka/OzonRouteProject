using System.Collections.Immutable;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonRoute.Domain.Exceptions.Infrastructure;
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

    public async Task<long[]> Clear(long userId, long[] calculationIds, CancellationToken cancellationToken)
    {   
        long[] calculationGoodIds = calculationIds.Length == 0 ?
                            await ClearFull(userId, cancellationToken) :
                            await ClearParticle(userId, calculationIds, cancellationToken);

        return calculationGoodIds;
    }

    private async Task<long[]> ClearFull(long userId, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
DELETE FROM calculations WHERE user_id = @UserId
    returning good_ids;
        ";

        var sqlQueryParams = new
        {
            UserId = userId
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var calculationGoodIds = await connection.QueryAsync<long[]>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );
        
        return ConvertToArray(calculationGoodIds);
    }

    private async Task<long[]> ClearParticle(long userId, long[] calculationIds, CancellationToken cancellationToken)
    {   
        //Not passing userId due to calculationIds valudation subsequently
        //Nothin will be missid caue of transaction mode in invalidation scenario + not spamming db with multiple queries
       const string sqlQuery = @"
DELETE FROM calculations WHERE id = ANY(@Ids)
    returning user_id, id, good_ids;
       ";

       var sqlQueryParams = new 
       {
            Ids = calculationIds
       };

       await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

       var deletedCalculations = await connection.QueryAsync<CalculationEntityV1>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
       );

       ValidatePassedCalculationIds(deletedCalculations, userId, calculationIds.Length);

       return ConvertToArray(deletedCalculations.Select(x => x.GoodIds)); 
    }

    private static long[] ConvertToArray(IEnumerable<long[]> jaggedArray)
    {
        return jaggedArray.SelectMany(x => x).ToArray();
    }

    public async Task<IReadOnlyList<CalculationEntityV1>> QueryByIds(long userId, long[] calculationIds, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
SELECT * FROM calculations WHERE id = ANY(@Ids);
        ";

        var sqlQueryParams = new
        {
            Ids = calculationIds
        };

        await using NpgsqlConnection connection = await GetAndOpenConnectionAsync(cancellationToken);

        var queriedCalculations = await connection.QueryAsync<CalculationEntityV1>(
            new CommandDefinition(
                commandText: sqlQuery,
                parameters: sqlQueryParams,
                cancellationToken: cancellationToken
            )
        );

        ValidatePassedCalculationIds(queriedCalculations, userId, calculationIds.Length);

        return queriedCalculations.ToList();
    }

    private static void ValidatePassedCalculationIds(IEnumerable<CalculationEntityV1> involvedCalculations, long userId, long expectedNumOfCalculations)
    {
        if (involvedCalculations.Count() != expectedNumOfCalculations)
        {
            throw new OneOrManyCalculationsNotFoundException("One or many calculations not found.");
        }

        long[] invalidUserIds = involvedCalculations.Select(x => x.UserId).Where(id => id != userId).Distinct().ToArray();

        if (invalidUserIds.Length != 0)
        {
            long[] invalidCalculationIds = involvedCalculations.Where(x => x.UserId != userId).Select(x => x.Id).ToArray();
            
            throw new OneOrManyCalculationsBelongToAnotherUserException(
                wrongCalculationIds: invalidCalculationIds,
                message: "One or many calculations belong to another user."
            );
        }
    }
}
