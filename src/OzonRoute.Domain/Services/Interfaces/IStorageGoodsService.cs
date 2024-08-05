using OzonRoute.Domain.Models;
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Services.Interfaces;

public interface IStorageGoodsService
{
    public Task<IReadOnlyList<StorageGoodModel>> GetGoodsFromStorage(CancellationToken cancellationToken);
    public Task UpdateGoods(IEnumerable<StorageGoodEntity> goodEntities, CancellationToken cancellationToken);
    public Task<double> CalculateFullPrice(IPriceCalculatorService priceCalculatorService, int id, CancellationToken cancellationToken);
}