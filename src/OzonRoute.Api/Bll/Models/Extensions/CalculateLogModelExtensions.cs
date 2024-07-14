using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Bll.Models.Extensions;
public static class CalculateLogModelExtensions
{   
    private const double gramsToKgRatio = 1000.0d;
    public static CalculateLogModel MapEntityToModel(this GoodPriceEntity goodPriceEntity)
    {
        return new CalculateLogModel(
            At: goodPriceEntity.At,
            Volume: goodPriceEntity.Volume,
            Weight: goodPriceEntity.Weight / gramsToKgRatio,
            Price: goodPriceEntity.Price,
            Distance: goodPriceEntity.Distance
        );
    }

    public static async Task<List<CalculateLogModel>> MapEntitiesToModels(this IEnumerable<GoodPriceEntity> goodPriceEntities)
    {
        return await Task.FromResult(goodPriceEntities.Select(g => g.MapEntityToModel()).ToList());
    }
}