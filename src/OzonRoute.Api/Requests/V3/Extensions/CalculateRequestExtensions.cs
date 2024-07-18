using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Requests.V3.Extensions;

public static class CalculateRequestExtensions
{   
    private const int mToCMetrsRatio = 100;
    public static async Task<IReadOnlyList<GoodModel>> MapRequestToModel(this CalculateRequest request)
    {   
        IEnumerable<GoodModel> result = request.Goods.Select(g => new GoodModel(
            Lenght: g.Lenght * mToCMetrsRatio,
            Width: g.Width * mToCMetrsRatio,
            Height: g.Height * mToCMetrsRatio,
            Weight: g.Weight
        ));
        return await Task.FromResult(result.ToList());
    }
}