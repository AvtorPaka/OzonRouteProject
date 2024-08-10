using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Models.Extensions;
public static class StorageGoodModelExtensions
{
    public static StorageGoodEntity MapModelToEntity(this StorageGoodModel storageGoodModel)
    {
        return new StorageGoodEntity(
            Name: storageGoodModel.Name.TrimEnd(),
            Id: storageGoodModel.Id,
            Count: storageGoodModel.Count,
            Length: storageGoodModel.Length,
            Width: storageGoodModel.Width,
            Height: storageGoodModel.Height,
            Weight: storageGoodModel.Weight,
            Price: storageGoodModel.Price
        );
    }

    public static async Task<IEnumerable<StorageGoodEntity>> MapModelsToEntitys(this IEnumerable<StorageGoodModel> storageGoodModels)
    {
        return await Task.FromResult(storageGoodModels.Select(m => m.MapModelToEntity()));
    }

    public static StorageGoodModel MapEntityToModel(this StorageGoodEntity goodEntity)
    {
        return new StorageGoodModel(
            Name: goodEntity.Name.TrimEnd(),
            Id: goodEntity.Id,
            Count: goodEntity.Count,
            Length: goodEntity.Length,
            Width: goodEntity.Width,
            Height: goodEntity.Height,
            Weight: goodEntity.Weight,
            Price: goodEntity.Price
        );
    }

    public static async Task<IReadOnlyList<StorageGoodModel>> MapEntitysToModels(this IEnumerable<StorageGoodEntity> goodEntities)
    {
        return await Task.FromResult(goodEntities.Select(e => e.MapEntityToModel()).ToList());
    }
}