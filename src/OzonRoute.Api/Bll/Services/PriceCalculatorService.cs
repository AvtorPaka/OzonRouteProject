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

    public double CalculatePrice(List<GoodModel> goods)
    {
        if (goods.Count() == 0) {
            throw new ArgumentException("Goods count supposed to be greater than 0");
        }

        double volumePrice = CalculatePriceByVolume(goods, out double summaryVolume);
        double weightPrice = CalculatePriceByWeight(goods, out double summaryWeight);
        double finalPrice = Math.Max(volumePrice, weightPrice);

        _goodPriceRepository.Save(new GoodPriceEntity(
            Price: finalPrice,
            Volume: summaryVolume,
            Weight: summaryWeight,
            At: DateTime.UtcNow));

        return finalPrice;
    }

    private static double CalculatePriceByVolume(List<GoodModel> goods, out double summaryVolume)
    {
        summaryVolume = goods.Sum(g => g.Lenght * g.Width * g.Height) / Math.Pow(10.0, 3.0);
        double volumePrice = summaryVolume * volumeRatio;

        return volumePrice;
    }

    private static double CalculatePriceByWeight(List<GoodModel> goods, out double summaryWeight)
    {
        summaryWeight = goods.Sum(g => g.Weight) * 1000.0d;
        double weightPrice = summaryWeight * weightRatio;

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