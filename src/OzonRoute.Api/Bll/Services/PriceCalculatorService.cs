using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Dal.Models;
using OzonRoute.Api.Bll.Models.Extensions;

namespace OzonRoute.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private const double volumeRatio = 3.27d;
    private readonly IGoodPriceRepository _goodPriceRepository;

    public PriceCalculatorService([FromServices] IGoodPriceRepository goodPriceRepository)
    {
        _goodPriceRepository = goodPriceRepository;
    }

    public double CalculatePrice(List<GoodModel> goods)
    {
        if (goods.Count() == 0) {
            throw new ArgumentException("Goods count supposed to be greater then 0");
        }

        double summaryVolume = goods.Sum(g => g.Lenght * g.Width * g.Height) / Math.Pow(10.0, 3.0);
        double price = summaryVolume * volumeRatio;

        _goodPriceRepository.Save(new GoodPriceEntity(
            Price: price,
            Volume: summaryVolume,
            At: DateTime.UtcNow));

        return price;
    }

    public async Task<List<CalculateLogModel>> QueryLog(int take)
    {   
        if (take <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take, "take parametr supposed to be greater then 0");
        }

        List<GoodPriceEntity> log = await _goodPriceRepository.QueryData();
        List<CalculateLogModel> processedLog = await log.OrderByDescending(g => g.At).Take(take).MapEntitiesToModels();

        return processedLog;
    }
}