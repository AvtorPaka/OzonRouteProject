using System.Security.Cryptography;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.Services;

public class GoodsService : IGoodsService
{
    private readonly IGoodsRepository _goodsRepository;

    public GoodsService(IGoodsRepository goodsRepository)
    {
        _goodsRepository = goodsRepository;
    }

    public async Task<IReadOnlyList<GoodStoreModel>> GetGoodsFromData(CancellationToken cancellationToken)
    {
        var goodEntities = await _goodsRepository.GetAllGoods();
        return await goodEntities.MapEntitysToModels();
    }

    public async Task UpdateGoods(IEnumerable<GoodEntity> goodEntities, CancellationToken cancellationToken)
    {
        foreach (var good in goodEntities)
        {
            await _goodsRepository.AddOrUpdate(good, cancellationToken);
        }
    }

    public async Task<GoodEntity> GetGoodFromData(int id)
    {
        return await _goodsRepository.Get(id);
    }
}