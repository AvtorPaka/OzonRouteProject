using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Models.Extensions;
public static class GoodModelExtensions
{
    private const double cm3ToM3Ratio = 1000000.0d;
    public static GoodEntityReport MapModelToEntity(this GoodModel goodModel)
    {
        return new GoodEntityReport(
            Volume: goodModel.Lenght * goodModel.Width * goodModel.Height / cm3ToM3Ratio,
            Weight: goodModel.Weight
        );
    }

    public static async Task<IReadOnlyList<GoodEntityReport>> MapModelsToEntitys(this IReadOnlyList<GoodModel> goodModels)
    {
        return await Task.FromResult(goodModels.Select(g => g.MapModelToEntity()).ToList());
    }
}