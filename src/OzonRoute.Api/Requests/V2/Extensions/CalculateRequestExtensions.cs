using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Requests.V2.Extensions;

public static class CalculateRequestExtensions
{   
    private const int mmToCMetrsRatio = 10;
    private static IReadOnlyList<GoodModel> MapRequestToModels(this CalculateRequest request)
    {
        IEnumerable<GoodModel> result = request.Goods.Select(g => new GoodModel(
            Lenght: g.Lenght/ mmToCMetrsRatio,
            Width: g.Width / mmToCMetrsRatio,
            Height: g.Height / mmToCMetrsRatio,
            Weight: g.Weight
        ));
        return result.ToList();
    }

    public static async Task<GoodModelsContainer> MapRequestToModelsContainer(this CalculateRequest request)
    {
        return await Task.FromResult(new GoodModelsContainer(
            UserId: request.UserId,
            Goods: request.MapRequestToModels(),
            Distance: 1000
        ));
    }
}