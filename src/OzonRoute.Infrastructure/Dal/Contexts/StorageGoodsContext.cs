using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Dal.Contexts;

//TODO: Cut that shit off and add another table
internal sealed class StorageGoodsContext
{
    public Dictionary<int, StorageGoodEntity> Store { get; init; } = new Dictionary<int, StorageGoodEntity>();
}