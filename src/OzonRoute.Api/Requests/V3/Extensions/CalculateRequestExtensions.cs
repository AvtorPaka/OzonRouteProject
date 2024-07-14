using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Requests.V3.Extensions;

public static class CalculateRequestExtensions
{   
    private const int metrsToMmRatio = 1000;

    public static async Task<List<GoodModel>> MapRequestToModel(this CalculateRequest request)
    {   
        IEnumerable<GoodModel> result = request.Goods.Select(g => new GoodModel(
            Lenght: g.Lenght * metrsToMmRatio,
            Width: g.Width * metrsToMmRatio,
            Height: g.Height * metrsToMmRatio,
            Weight: g.Weight
        ));
        return await Task.FromResult(result.ToList());
    }
}