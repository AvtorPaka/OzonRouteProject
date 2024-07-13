using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Requests.V2.Extensions;

public static class CalculateRequestExtensions
{
    public static async Task<List<GoodModel>> MapRequestToModel(this CalculateRequest request)
    {
        IEnumerable<GoodModel> result = request.Goods.Select(g => new GoodModel(
            Lenght: g.Lenght,
            Width: g.Width,
            Height: g.Height,
            Weight: g.Weight
        ));
        return await Task.FromResult(result.ToList());
    }
}