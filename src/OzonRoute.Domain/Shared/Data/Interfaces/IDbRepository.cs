using System.Transactions;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface IDbRepository
{
    public TransactionScope CreateTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}