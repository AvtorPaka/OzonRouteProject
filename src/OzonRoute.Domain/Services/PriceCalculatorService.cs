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
using OzonRoute.Domain.Exceptions.Infrastructure;

namespace OzonRoute.Domain.Services;

internal class PriceCalculatorService : IPriceCalculatorService
{
    private readonly double _volumeToPriceRatio;
    private readonly double _weightToPriceRatio;
    private readonly ICalculationsRepository _calculationsRepository;
    private readonly ICalculationGoodsRepository  _calculationGoodsRepository;

    public PriceCalculatorService(
        PriceCalculatorOptions options,
        ICalculationsRepository calculationsRepository,
        ICalculationGoodsRepository calculationGoodsRepository)
    {
        _volumeToPriceRatio = options.VolumeToPriceRatio;
        _weightToPriceRatio = options.WeightToPriceRatio;
        _calculationsRepository = calculationsRepository;
        _calculationGoodsRepository = calculationGoodsRepository;
    }

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
        catch (EntityNotFoundException ex)
        {
            throw new DomainException("Invalid input data", ex);
        }
    }

    private async Task<double> CalculatePriceUnsafe(DeliveryGoodsContainer deliveryGoodsContainer, CancellationToken cancellationToken)
    {
        var validator = new DeliveryGoodsContainerValidator();
        await validator.ValidateAndThrowAsync(deliveryGoodsContainer, cancellationToken);

        double finalPrice = CalculatePriceForOneMetr(deliveryGoodsContainer.Goods , out double summaryVolume, out double summaryWeight) * deliveryGoodsContainer.Distance;

        await SaveCalculationsData(
            saveModel: new SaveCalculationModel(
                GoodsContainer: deliveryGoodsContainer,
                TotalVolume: summaryVolume,
                TotalWeight: summaryWeight,
                Price: (decimal)finalPrice,
                At: DateTime.UtcNow
            ),
            token: cancellationToken
        );

        return finalPrice;
    }

    private async Task SaveCalculationsData(SaveCalculationModel saveModel, CancellationToken token)
    {   
        using var transaction = _calculationsRepository.CreateTransactionScope();

        long[] calculationGoodsIds = await _calculationGoodsRepository.Add(
            saveModel.GoodsContainer.Goods.MapModelsToEntities(saveModel.GoodsContainer.UserId) ,
            token);

        await _calculationsRepository.Add(
            [new CalculationEntityV1{
                UserId = saveModel.GoodsContainer.UserId,
                GoodIds = calculationGoodsIds,
                TotalVolume = saveModel.TotalVolume,
                TotalWeight = saveModel.TotalWeight,
                Price = saveModel.Price,
                Distance = saveModel.GoodsContainer.Distance,
                At = saveModel.At
            }],
            cancellationToken: token);

        transaction.Complete();
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


    private async Task<IReadOnlyList<CalculationLogModel>> QueryLogUnsafe(GetHistoryModel model, CancellationToken cancellationToken)
    {
        var validator = new GetHistoryModelValidator();
        await validator.ValidateAndThrowAsync(model, cancellationToken);

        IReadOnlyList<CalculationEntityV1> log = await _calculationsRepository.Query(model.MapModelToDalModel(), cancellationToken);
        IReadOnlyList<CalculationLogModel> processedLog = await log.MapEntitiesToModels();

        return processedLog;
    }

    public void ClearLog()
    {
        _calculationsRepository.ClearData();
    }
}