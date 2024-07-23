using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Requests.V3.Extensions;

public static class CalculateRequestExtensions
{   
    private const int mToCMetrsRatio = 100;
    private static IReadOnlyList<GoodModel> MapRequestToModels(this CalculateRequest request)
    {
        IEnumerable<GoodModel> result = request.Goods.Select(g => new GoodModel(
            Lenght: g.Lenght * mToCMetrsRatio,
            Width: g.Width * mToCMetrsRatio,
            Height: g.Height * mToCMetrsRatio,
            Weight: g.Weight
        ));
        return result.ToList();
    }

    public static async Task<GoodModelsContainer> MapRequestToModelsContainer(this CalculateRequest request)
    {
        return await Task.FromResult(new GoodModelsContainer(
            Goods: request.MapRequestToModels(),
            Distance: request.Distance
        ));
    }
}