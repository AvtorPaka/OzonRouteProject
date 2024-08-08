using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Models;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface ICalculationsRepository: IDbRepository
{
    public Task<long[]> Add(CalculationEntityV1[] entityV1, CancellationToken cancellationToken);
    public Task<long[]> Clear(long userId, long[] calculationIds, CancellationToken cancellationToken);
    public Task<IReadOnlyList<CalculationEntityV1>> Query(CalculationHistoryQueryModel model, CancellationToken cancellationToken);
}