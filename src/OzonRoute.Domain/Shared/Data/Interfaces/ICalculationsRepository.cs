using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Models;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface ICalculationsRepository: IDbRepository
{
    public Task<long[]> Add(CalculationEntityV1[] entityV1, CancellationToken cancellationToken);
    public void ClearData();
    public Task<IReadOnlyList<CalculationEntityV1>> Query(CalculationHistoryQueryModel model, CancellationToken cancellationToken);
}