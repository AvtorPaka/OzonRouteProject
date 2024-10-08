using System.Security.Cryptography;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Infrastructure.Services.Interfaces;

namespace OzonRoute.Infrastructure.Services;

internal sealed class GoodsDetachedService : IGoodsDetachedService
{
    //In educational purposes
    private readonly List<StorageGoodModel> _goods = new List<StorageGoodModel>()
    {
        new StorageGoodModel(Name: "Парик для питомца", Id: 1, Height: 100.0, Length: 200.0, Width: 300.0, Weight: 4,  Price: 100, Count: RandomNumberGenerator.GetInt32(11)),
        new StorageGoodModel(Name: "Накидка на телевизор", Id: 2, Height: 100.0, Length: 200.0, Width: 300.0, Weight: 1 ,Price: 120, Count: RandomNumberGenerator.GetInt32(11)),
        new StorageGoodModel(Name: "Ковер настенный", Id: 3, Height: 200.0, Length: 300.0, Width: 300.0, Weight: 5 ,Price: 140, Count: RandomNumberGenerator.GetInt32(11)),
        new StorageGoodModel(Name: "Здоровенный язь", Id: 4, Height: 100.0, Length: 100.0, Width: 400.0, Weight: 10,Price: 160, Count: RandomNumberGenerator.GetInt32(11)),
        new StorageGoodModel(Name: "Билет МММ", Id: 5, Height: 10.0, Length: 10.0, Width: 15.0, Weight: 0.1 ,Price: 180, Count: RandomNumberGenerator.GetInt32(11))
    };

    public async Task<IEnumerable<StorageGoodEntity>> GetGoodsFromDetached(CancellationToken cancellationToken)
    {
        return await _goods.MapModelsToEntitys();
    }
}