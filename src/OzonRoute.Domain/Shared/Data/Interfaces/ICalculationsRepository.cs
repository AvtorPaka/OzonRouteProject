using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface ICalculationsRepository
{
    public void Save(CalculationEntityV1 goodPriceData);
    public void ClearData();
    public Task<IReadOnlyList<CalculationEntityV1>> QueryData(CancellationToken cancellationToken);
}