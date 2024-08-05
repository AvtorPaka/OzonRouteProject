using FluentValidation;
using OzonRoute.Domain.Exceptions.Domain;
using OzonRoute.Domain.Exceptions.Infrastructure;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.Services;

internal sealed class GoodsService : IGoodsService
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

    public async Task<double> CalculateFullPrice(IPriceCalculatorService priceCalculatorService, int id, CancellationToken cancellationToken)
    {
        try
        {
            return await CalculateFullPriceUnsafe(priceCalculatorService, id, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw new DomainException("Invalid input data", ex);
        }
        catch (EntityNotFoundException ex)
        {
            throw new DomainException("Invalid input data", ex);
        }
    }

    private async Task<double> CalculateFullPriceUnsafe(IPriceCalculatorService priceCalculatorService, int id, CancellationToken cancellationToken)
    {
        GoodEntity entity = await _goodsRepository.Get(id);
        GoodModel goodModel = new GoodModel(
            Lenght: entity.Lenght,
            Width: entity.Width,
            Height: entity.Height,
            Weight: entity.Weight
        );

        // TODO: Do smth with UserId
        double shipPrice = await priceCalculatorService.CalculatePrice(
            new GoodModelsContainer(
                UserId: int.MaxValue/2,
                Goods: [goodModel],
                Distance: 1000
            ),
            cancellationToken);
            
        double finalPrice = shipPrice + entity.Price;

        return finalPrice;
    }

}