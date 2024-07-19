using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Context;

public class GoodsContext
{
    public Dictionary<int, GoodEntity> Store { get; init; } = new Dictionary<int, GoodEntity>();
}