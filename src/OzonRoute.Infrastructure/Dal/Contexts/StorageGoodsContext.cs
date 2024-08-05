using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Dal.Contexts;

internal sealed class StorageGoodsContext
{
    public Dictionary<int, StorageGoodEntity> Store { get; init; } = new Dictionary<int, StorageGoodEntity>();
}