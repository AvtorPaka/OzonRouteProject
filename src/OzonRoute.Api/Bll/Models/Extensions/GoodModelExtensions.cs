using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Bll.Models.Extensions;
public static class GoodModelExtensions
{
    private const double mm3ToM3Ratio = 1000000000.0d;

    public static GoodEntity MapModelToEntity(this GoodModel goodModel)
    {
        return new GoodEntity(
            Volume: (goodModel.Lenght * goodModel.Width * goodModel.Height) / mm3ToM3Ratio,
            Weight: goodModel.Weight
        );
    }

    public static async Task<List<GoodEntity>> MapModelsToEntitys(this List<GoodModel> goodModels)
    {
        return await Task.FromResult(goodModels.Select(g => g.MapModelToEntity()).ToList());
    }
}