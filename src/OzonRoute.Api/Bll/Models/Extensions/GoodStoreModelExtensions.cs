using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Bll.Models.Extensions;
public static class GoodStoreModelExtensions
{
    public static GoodEntity MapModelToEntity(this GoodStoreModel goodStoreModel)
    {
        return new GoodEntity(
            Name: goodStoreModel.Name,
            Id: goodStoreModel.Id,
            Count: goodStoreModel.Count,
            Lenght: goodStoreModel.Lenght,
            Width: goodStoreModel.Width,
            Height: goodStoreModel.Height,
            Weight: goodStoreModel.Weight,
            Price: goodStoreModel.Price
        );
    }

    public static async Task<IEnumerable<GoodEntity>> MapModelsToEntitys(this IEnumerable<GoodStoreModel> goodStoreModels)
    {
        return await Task.FromResult(goodStoreModels.Select(m => m.MapModelToEntity()));
    }

    public static GoodStoreModel MapEntityToModel(this GoodEntity goodEntity)
    {
        return new GoodStoreModel(
            Name: goodEntity.Name,
            Id: goodEntity.Id,
            Count: goodEntity.Count,
            Lenght: goodEntity.Lenght,
            Width: goodEntity.Width,
            Height: goodEntity.Height,
            Weight: goodEntity.Weight,
            Price: goodEntity.Price
        );
    }

    public static async Task<IReadOnlyList<GoodStoreModel>> MapEntitysToModels(this IEnumerable<GoodEntity> goodEntities)
    {
        return await Task.FromResult(goodEntities.Select(e => e.MapEntityToModel()).ToList());
    }
}