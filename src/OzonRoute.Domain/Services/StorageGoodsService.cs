using FluentValidation;
using Microsoft.Extensions.Logging;
using OzonRoute.Domain.Exceptions.Domain;
using OzonRoute.Domain.Exceptions.Infrastructure;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.Services;

internal sealed class StorageGoodsService : IStorageGoodsService
{
    private readonly IStorageGoodsRepository _storageGoodsRepository;

    private readonly ILogger<StorageGoodsService> _logger;

    public StorageGoodsService(IStorageGoodsRepository storageGoodsRepository, ILogger<StorageGoodsService> logger)
    {
        _storageGoodsRepository = storageGoodsRepository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<StorageGoodModel>> QueryGoods(CancellationToken cancellationToken)
    {
        var goodEntities = await _storageGoodsRepository.Query(cancellationToken);
        return await goodEntities.MapEntitysToModels();
    }

    public async Task UpdateGoods(IEnumerable<StorageGoodEntity> goodEntities, CancellationToken cancellationToken)
    {   
        using var transaction = _storageGoodsRepository.CreateTransactionScope();

        await _storageGoodsRepository.AddOrUpdate(goodEntities.ToArray(), cancellationToken);

        transaction.Complete();
    }

    public async Task<double> CalculateFullPrice(IPriceCalculatorService priceCalculatorService, long id, CancellationToken cancellationToken)
    {
        try
        {
            return await CalculateFullPriceUnsafe(priceCalculatorService, id, cancellationToken);
        }
        catch (ValidationException ex)
        {   
            _logger.LogError(ex, "Invalid input data");
            throw new DomainException("Invalid input data.", ex);
        }
        catch (EntityNotFoundException ex)
        {   
            _logger.LogError(ex, "Invalid input data");
            throw new DomainException("Invalid input data.", ex);
        }
    }

    private async Task<double> CalculateFullPriceUnsafe(IPriceCalculatorService priceCalculatorService, long id, CancellationToken cancellationToken)
    {
        StorageGoodEntity entity = await _storageGoodsRepository.QuerySingle(id, cancellationToken);

        DeliveryGoodModel goodModel = new DeliveryGoodModel(
            Lenght: entity.Length,
            Width: entity.Width,
            Height: entity.Height,
            Weight: entity.Weight
        );

        var saveModel = await priceCalculatorService.CalculatePrice(
            new DeliveryGoodsContainer(
                UserId: int.MaxValue/2,
                Goods: [goodModel],
                Distance: 1000
            ),
            cancellationToken);
            
        double finalPrice = (double)saveModel.Price + (double)entity.Price;

        return finalPrice;
    }

}