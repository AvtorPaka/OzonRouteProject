using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Bll.Models.Extensions;
using OzonRoute.Api.Dal.Repositories.Interfaces;
using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private const double volumeRatio = 3.27d;
    private const double weightRatio = 1.34d;
    private readonly IGoodPriceRepository _goodPriceRepository;

    public PriceCalculatorService([FromServices] IGoodPriceRepository goodPriceRepository)
    {
        _goodPriceRepository = goodPriceRepository;
    }

    public double CalculatePrice(List<GoodModel> goods, double distance = 1000)
    {
        if (!goods.Any()) {
            throw new ArgumentException("Goods count must be greater than 0");
        }
        if (distance <= 1) {
            throw new ArgumentException("Shipping distance must be greater than 1");
        }

        double finalPrice = CalculatePriceForOneMetr(goods, out double summaryVolume, out double summaryWeight) * distance;

        _goodPriceRepository.Save(new GoodPriceEntity(
            Price: finalPrice,
            Volume: summaryVolume, // In mm^3
            Weight: summaryWeight,  // In gramms
            Distance: distance, // In metrs
            At: DateTime.UtcNow));

        return finalPrice;
    }

    private double CalculatePriceForOneMetr(List<GoodModel> goods, out double summaryVolume, out double summaryWeight)
    {
        double volumePrice = CalculatePriceByVolume(goods, out summaryVolume);
        double weightPrice = CalculatePriceByWeight(goods, out summaryWeight);
        double priceForOneMetr = Math.Max(volumePrice, weightPrice) / 1000;

        return priceForOneMetr;
    }

    private static double CalculatePriceByVolume(List<GoodModel> goods, out double summaryVolume)
    {
        summaryVolume = goods.Sum(g => g.Lenght * g.Width * g.Height);
        double volumePrice = summaryVolume  / Math.Pow(10.0, 3.0) * volumeRatio;

        return volumePrice;
    }

    private static double CalculatePriceByWeight(List<GoodModel> goods, out double summaryWeight)
    {
        summaryWeight = goods.Sum(g => g.Weight) * 1000.0d;
        double weightPrice = (summaryWeight * weightRatio) / 1000.0d;

        return weightPrice;
    }

    public async Task<List<CalculateLogModel>> QueryLog(int take)
    {   
        if (take <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take, "take parametr supposed to be greater than 0");
        }

        List<GoodPriceEntity> log = await _goodPriceRepository.QueryData();
        List<CalculateLogModel> processedLog = await log.OrderByDescending(g => g.At).Take(take).MapEntitiesToModels();

        return processedLog;
    }
}