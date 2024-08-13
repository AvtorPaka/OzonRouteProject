using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Models.Extensions;
public static class CalculationLogModelExtensions
{
    private const double gramsToKgRatio = 1000.0d;
    public static CalculationLogModel MapEntityToModel(this CalculationEntityV1 calculationEntityV1)
    {
        return new CalculationLogModel(
            Id: calculationEntityV1.Id,
            UserId: calculationEntityV1.UserId,
            GoodsIds: calculationEntityV1.GoodIds,
            TotalVolume: calculationEntityV1.TotalVolume,
            TotalWeight: calculationEntityV1.TotalWeight / gramsToKgRatio,
            Price: calculationEntityV1.Price,
            Distance: calculationEntityV1.Distance,
            At: calculationEntityV1.At
        );
    }

    public static async Task<IReadOnlyList<CalculationLogModel>> MapEntitiesToModels(this IEnumerable<CalculationEntityV1> goodPriceEntities)
    {
        return await Task.FromResult(goodPriceEntities.Select(g => g.MapEntityToModel()).ToList());
    }
}