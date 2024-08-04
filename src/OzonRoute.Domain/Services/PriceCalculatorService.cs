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
    private readonly IGoodPriceRepository _goodPriceRepository;

    public PriceCalculatorService(
        PriceCalculatorOptions options,
        IGoodPriceRepository goodPriceRepository)
    {
        _volumeToPriceRatio = options.VolumeToPriceRatio;
        _weightToPriceRatio = options.WeightToPriceRatio;
        _goodPriceRepository = goodPriceRepository;
    }

    //TODO: Change QUERYING Data
    public async Task<double> CalculatePrice(GoodModelsContainer goodModelsContainer, CancellationToken cancellationToken)
    {   
        try
        {
            return await CalculatePriceUnsafe(goodModelsContainer, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw new DomainException("Invalid input data", ex);
        }
    }

    //TODO: Change QUERYING Data
    private async Task<double> CalculatePriceUnsafe(GoodModelsContainer goodModelsContainer, CancellationToken cancellationToken)
    {
        var validator = new GoodModelsContainerValidator();
        await validator.ValidateAndThrowAsync(goodModelsContainer, cancellationToken);

        double finalPrice = CalculatePriceForOneMetr(goodModelsContainer.Goods , out double summaryVolume, out double summaryWeight) * goodModelsContainer.Distance;

        _goodPriceRepository.Save(new GoodPriceEntity(
            Price: finalPrice,
            Volume: summaryVolume, // In cm^3
            Weight: summaryWeight,  // In gramms
            Distance: goodModelsContainer.Distance, // In metrs
            At: DateTime.UtcNow));

        return finalPrice;
    }

    private double CalculatePriceForOneMetr(IReadOnlyList<GoodModel> goods, out double summaryVolume, out double summaryWeight)
    {
        double volumePrice = CalculatePriceByVolume(goods, out summaryVolume);
        double weightPrice = CalculatePriceByWeight(goods, out summaryWeight);
        double priceForOneMetr = Math.Max(volumePrice, weightPrice) / 1000;

        return priceForOneMetr;
    }

    private double CalculatePriceByVolume(IReadOnlyList<GoodModel> goods, out double summaryVolume)
    {
        summaryVolume = goods.Sum(g => g.Lenght * g.Width * g.Height);
        double volumePrice = summaryVolume * _volumeToPriceRatio;

        return volumePrice;
    }

    private double CalculatePriceByWeight(IReadOnlyList<GoodModel> goods, out double summaryWeight)
    {
        summaryWeight = goods.Sum(g => g.Weight) * 1000.0d;
        double weightPrice = summaryWeight * _weightToPriceRatio / 1000.0d;

        return weightPrice;
    }

    //TODO: Change impementation
    public async Task<IReadOnlyList<CalculateLogModel>> QueryLog(GetHistoryModel model, CancellationToken cancellationToken)
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
    private async Task<IReadOnlyList<CalculateLogModel>> QueryLogUnsafe(GetHistoryModel model, CancellationToken cancellationToken)
    {
        var validator = new GetHistoryModelValidator();
        await validator.ValidateAndThrowAsync(model, cancellationToken);

        IReadOnlyList<GoodPriceEntity> log = await _goodPriceRepository.QueryData(cancellationToken);
        IReadOnlyList<CalculateLogModel> processedLog = await log.OrderByDescending(g => g.At).Take(model.Take).MapEntitiesToModels();

        return processedLog;
    }

    public void ClearLog()
    {
        _goodPriceRepository.ClearData();
    }
}