using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Responses.V1.Extensions;

public static class GetGoodsResponseExtensions
{   
    private const double cmToMRatio = 100.0d;
    public static GetGoodsResponse MapModelToResponse(this GoodStoreModel goodStoreModel)
    {
        return new GetGoodsResponse(
            Name: goodStoreModel.Name,
            Id: goodStoreModel.Id,
            Count: goodStoreModel.Count,
            Lenght: goodStoreModel.Lenght / cmToMRatio,
            Width: goodStoreModel.Width / cmToMRatio,
            Height: goodStoreModel.Height / cmToMRatio,
            Weight: goodStoreModel.Weight,
            Price: goodStoreModel.Price
        );  
    }

    public static async Task<IReadOnlyList<GetGoodsResponse>> MapModelsToResponse(this IEnumerable<GoodStoreModel> goodStoreModels)
    {
        return await Task.FromResult(goodStoreModels.Select(m => m.MapModelToResponse()).ToList());
    }
}