using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Infrastructure.Dal.Infrastructure;

namespace OzonRoute.Infrastructure.Dal.Repositories;

public abstract class BaseRepository : IDbRepository
{
    private readonly NpgsqlDataSource _npgsqlDataSource;

    protected BaseRepository(NpgsqlDataSource dataSource)
    {
       _npgsqlDataSource = dataSource;
    }

    protected async Task<NpgsqlConnection> GetAndOpenConnectionAsync(
        CancellationToken cancellationToken)
    {
        var connection = await _npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        connection.ReloadTypes(); //In case smth change after MigrateUp()
                                  //normally you wouldn't do that or do migrations with another connection string, js lazy to create users
        return connection;
    }

    public TransactionScope CreateTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            scopeOption: TransactionScopeOption.Required,
            transactionOptions: new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = TimeSpan.FromSeconds(5)
            },
            asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled
        );
    }
}