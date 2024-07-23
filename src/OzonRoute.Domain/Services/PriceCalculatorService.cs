using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Domain.Configuration.Models;

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

    public async Task<double> CalculatePrice(IReadOnlyList<GoodModel> goods, int distance = 1000)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)); //Fiction

        double finalPrice = CalculatePriceForOneMetr(goods, out double summaryVolume, out double summaryWeight) * distance;

        _goodPriceRepository.Save(new GoodPriceEntity(
            Price: finalPrice,
            Volume: summaryVolume, // In cm^3
            Weight: summaryWeight,  // In gramms
            Distance: distance, // In metrs
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
    public async Task<IReadOnlyList<CalculateLogModel>> QueryLog(int take, CancellationToken cancellationToken)
    {
        if (take <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take, "take parametr supposed to be greater than 0");
        }

        IReadOnlyList<GoodPriceEntity> log = await _goodPriceRepository.QueryData(cancellationToken);
        IReadOnlyList<CalculateLogModel> processedLog = await log.OrderByDescending(g => g.At).Take(take).MapEntitiesToModels();

        return processedLog;
    }

    public void ClearLog()
    {
        _goodPriceRepository.ClearData();
    }
}