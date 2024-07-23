using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Dal.Contexts;

internal sealed class GoodsContext
{
    public Dictionary<int, GoodEntity> Store { get; init; } = new Dictionary<int, GoodEntity>();
}