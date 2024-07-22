using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface IGoodPriceRepository
{
    public void Save(GoodPriceEntity goodPriceData);
    public void ClearData();
    public Task<IReadOnlyList<GoodPriceEntity>> QueryData(CancellationToken cancellationToken);
}