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

    //In educational purposes
    private readonly List<GoodStoreModel> _goods = new List<GoodStoreModel>()
    {
        new GoodStoreModel(Name: "Парик для питомца", Id: 1, Height: 100.0, Lenght: 200.0, Width: 300.0, Weight: 4,  Price: 100, Count: RandomNumberGenerator.GetInt32(11)),
        new GoodStoreModel(Name: "Накидка на телевизор", Id: 2, Height: 100.0, Lenght: 200.0, Width: 300.0, Weight: 1 ,Price: 120, Count: RandomNumberGenerator.GetInt32(11)),
        new GoodStoreModel(Name: "Ковер настенный", Id: 3, Height: 200.0, Lenght: 300.0, Width: 300.0, Weight: 5 ,Price: 140, Count: RandomNumberGenerator.GetInt32(11)),
        new GoodStoreModel(Name: "Здоровенный язь", Id: 4, Height: 100.0, Lenght: 100.0, Width: 400.0, Weight: 10,Price: 160, Count: RandomNumberGenerator.GetInt32(11)),
        new GoodStoreModel(Name: "Билет МММ", Id: 5, Height: 10.0, Lenght: 10.0, Width: 15.0, Weight: 0.1 ,Price: 180, Count: RandomNumberGenerator.GetInt32(11))
    };

    public async Task<IReadOnlyList<GoodStoreModel>> GetGoodsFromData(CancellationToken cancellationToken)
    {
        var goodEntities = await _goodsRepository.GetAllGoods();
        return await goodEntities.MapEntitysToModels();
    }

    public async Task<IEnumerable<GoodEntity>> GetGoodsFromDetached(CancellationToken cancellationToken)
    {
        return await _goods.MapModelsToEntitys();
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