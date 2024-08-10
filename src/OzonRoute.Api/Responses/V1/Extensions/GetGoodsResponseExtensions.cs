using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Responses.V1.Extensions;

public static class GetGoodsResponseExtensions
{   
    private const double cmToMRatio = 100.0d;
    public static GetGoodsResponse MapModelToResponse(this StorageGoodModel storageGoodModel)
    {
        return new GetGoodsResponse(
            Name: storageGoodModel.Name,
            Id: storageGoodModel.Id,
            Count: storageGoodModel.Count,
            Length: storageGoodModel.Length / cmToMRatio,
            Width: storageGoodModel.Width / cmToMRatio,
            Height: storageGoodModel.Height / cmToMRatio,
            Weight: storageGoodModel.Weight,
            Price: storageGoodModel.Price
        );  
    }

    public static async Task<IReadOnlyList<GetGoodsResponse>> MapModelsToResponse(this IEnumerable<StorageGoodModel> storageGoodModels)
    {
        return await Task.FromResult(storageGoodModels.Select(m => m.MapModelToResponse()).ToList());
    }
}