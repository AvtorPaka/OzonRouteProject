using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Requests.V3.Extensions;

public static class CalculateRequestExtensions
{   
    private const int mToCMetrsRatio = 100;
    private static IReadOnlyList<DeliveryGoodModel> MapRequestToModels(this CalculateRequest request)
    {
        IEnumerable<DeliveryGoodModel> result = request.Goods.Select(g => new DeliveryGoodModel(
            Lenght: g.Lenght * mToCMetrsRatio,
            Width: g.Width * mToCMetrsRatio,
            Height: g.Height * mToCMetrsRatio,
            Weight: g.Weight
        ));
        return result.ToList();
    }

    public static async Task<DeliveryGoodsContainer> MapRequestToModelsContainer(this CalculateRequest request)
    {
        return await Task.FromResult(new DeliveryGoodsContainer(
            UserId: request.UserId,
            Goods: request.MapRequestToModels(),
            Distance: request.Distance
        ));
    }
}