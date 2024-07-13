using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Requests.V1.Extensions;

public static class CalculateRequestExtensions
{
    public static async Task<List<GoodModel>> MapRequestToModel(this CalculateRequest request)
    {
        IEnumerable<GoodModel> result = request.Goods.Select(g => new GoodModel(g.Lenght, g.Width, g.Height));
        return await Task.FromResult(result.ToList());
    }
}