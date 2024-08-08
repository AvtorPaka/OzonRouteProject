using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface ICalculationGoodsRepository: IDbRepository
{
    public Task<long[]> Add(CalculationGoodEntityV1[] calculationGoods, CancellationToken cancellationToken);
    public Task<IReadOnlyList<CalculationGoodEntityV1>> Query(long userId, CancellationToken cancellationToken);
    public Task<int> Clear(long userId, long[] calculationGoodIds, CancellationToken cancellationToken);
    
}