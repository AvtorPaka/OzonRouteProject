using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Requests.V1.Extensions;

public static class CalculateRequestExtensions
{
    private const int mmToCMetrsRatio = 10;
    public static async Task<List<GoodModel>> MapRequestToModel(this CalculateRequest request)
    {
        IEnumerable<GoodModel> result = request.Goods.Select(g => new GoodModel(
            Lenght: g.Lenght / mmToCMetrsRatio,
            Width: g.Width / mmToCMetrsRatio,
            Height: g.Height/ mmToCMetrsRatio));
        return await Task.FromResult(result.ToList());
    }
}