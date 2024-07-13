using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Bll.Models.Extensions;
public static class CalculateLogModelExtensions
{   
    public static CalculateLogModel MapEntityToModel(this GoodPriceEntity goodPriceEntity)
    {
        return new CalculateLogModel(
            At: goodPriceEntity.At,
            Volume: goodPriceEntity.Volume,
            Weight: goodPriceEntity.Weight,
            Price: goodPriceEntity.Price
        );
    }

    public static async Task<List<CalculateLogModel>> MapEntitiesToModels(this IEnumerable<GoodPriceEntity> goodPriceEntities)
    {
        return await Task.FromResult(goodPriceEntities.Select(g => g.MapEntityToModel()).ToList());
    }
}