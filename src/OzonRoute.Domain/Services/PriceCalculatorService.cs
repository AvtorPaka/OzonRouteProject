using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Validators;
using FluentValidation;
using OzonRoute.Domain.Exceptions;
using OzonRoute.Domain.Exceptions.Domain;

namespace OzonRoute.Domain.Services;

internal sealed class PriceCalculatorService : IPriceCalculatorService
{
    private readonly double _volumeToPriceRatio;
    private readonly double _weightToPriceRatio;
    private readonly ICalculationsRepository _calculationsRepository;

    public PriceCalculatorService(
        PriceCalculatorOptions options,
        ICalculationsRepository calculationsRepository)
    {
        _volumeToPriceRatio = options.VolumeToPriceRatio;
        _weightToPriceRatio = options.WeightToPriceRatio;
        _calculationsRepository = calculationsRepository;
    }

    //TODO: Change QUERYING Data
    public async Task<double> CalculatePrice(DeliveryGoodsContainer deliveryGoodsContainer, CancellationToken cancellationToken)
    {   
        try
        {
            return await CalculatePriceUnsafe(deliveryGoodsContainer, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw new DomainException("Invalid input data", ex);
        }
    }

    //TODO: Change QUERYING Data
    private async Task<double> CalculatePriceUnsafe(DeliveryGoodsContainer deliveryGoodsContainer, CancellationToken cancellationToken)
    {
        var validator = new DeliveryGoodsContainerValidator();
        await validator.ValidateAndThrowAsync(deliveryGoodsContainer, cancellationToken);

        double finalPrice = CalculatePriceForOneMetr(deliveryGoodsContainer.Goods , out double summaryVolume, out double summaryWeight) * deliveryGoodsContainer.Distance;

        _calculationsRepository.Save(new CalculationEntityV1(
                id: int.MaxValue/2,
                userId: deliveryGoodsContainer.UserId,
                goodIds: [],
                totalVolume: summaryVolume,
                totalWeight: summaryWeight,
                price: finalPrice,
                distance: deliveryGoodsContainer.Distance,
                at: DateTime.UtcNow
            ));

        return finalPrice;
    }

    private double CalculatePriceForOneMetr(IReadOnlyList<DeliveryGoodModel> goods, out double summaryVolume, out double summaryWeight)
    {
        double volumePrice = CalculatePriceByVolume(goods, out summaryVolume);
        double weightPrice = CalculatePriceByWeight(goods, out summaryWeight);
        double priceForOneMetr = Math.Max(volumePrice, weightPrice) / 1000;

        return priceForOneMetr;
    }

    private double CalculatePriceByVolume(IReadOnlyList<DeliveryGoodModel> goods, out double summaryVolume)
    {
        summaryVolume = goods.Sum(g => g.Lenght * g.Width * g.Height);
        double volumePrice = summaryVolume * _volumeToPriceRatio;

        return volumePrice;
    }

    private double CalculatePriceByWeight(IReadOnlyList<DeliveryGoodModel> goods, out double summaryWeight)
    {
        summaryWeight = goods.Sum(g => g.Weight) * 1000.0d;
        double weightPrice = summaryWeight * _weightToPriceRatio / 1000.0d;

        return weightPrice;
    }

    //TODO: Change impementation
    public async Task<IReadOnlyList<CalculationLogModel>> QueryLog(GetHistoryModel model, CancellationToken cancellationToken)
    {
        try
        {
            return await QueryLogUnsafe(model, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw new DomainException("Invalid input data", ex);
        }
    }


    //TODO: Change impementation
    private async Task<IReadOnlyList<CalculationLogModel>> QueryLogUnsafe(GetHistoryModel model, CancellationToken cancellationToken)
    {
        var validator = new GetHistoryModelValidator();
        await validator.ValidateAndThrowAsync(model, cancellationToken);

        IReadOnlyList<CalculationEntityV1> log = await _calculationsRepository.QueryData(cancellationToken);
        IReadOnlyList<CalculationLogModel> processedLog = await log.OrderByDescending(g => g.At).Take(model.Take).MapEntitiesToModels();

        return processedLog;
    }

    public void ClearLog()
    {
        _calculationsRepository.ClearData();
    }
}