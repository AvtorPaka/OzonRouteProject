using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Bll.Models.Extensions;
using OzonRoute.Api.Dal.Repositories.Interfaces;
using OzonRoute.Api.Dal.Models;
using Microsoft.Extensions.Options;
using OzonRoute.Api.Configuration.Models;

namespace OzonRoute.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private readonly double _volumeToPriceRatio;
    private readonly double _weightToPriceRatio;
    private readonly IGoodPriceRepository _goodPriceRepository;
    private readonly IReportsRepository _reportsRepository;

    public PriceCalculatorService(
        [FromServices] IOptionsSnapshot<PriceCalculatorOptions> options,
        [FromServices] IGoodPriceRepository goodPriceRepository,
        [FromServices] IReportsRepository reportsRepository)
    {   
        _volumeToPriceRatio = options.Value.VolumeToPriceRatio;
        _weightToPriceRatio = options.Value.WeightToPriceRatio;
        _goodPriceRepository = goodPriceRepository;
        _reportsRepository = reportsRepository;
    }

    public async Task<double> CalculatePrice(IReadOnlyList<GoodModel> goods, int distance = 1000)
    {
        if (!goods.Any()) {
            throw new ArgumentException("Goods count must be greater than 0");
        }
        if (distance <= 1) {
            throw new ArgumentException("Shipping distance must be greater than 1");
        }

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
        double weightPrice = (summaryWeight * _weightToPriceRatio) / 1000.0d;

        return weightPrice;
    }

    public async Task CalculateNewReportData(IReadOnlyList<GoodModel> goods, int distance, double finalPrice)
    {
        IReadOnlyList<GoodEntityReport> goodsEntities = await goods.MapModelsToEntitys();
        _reportsRepository.CalculateNewMaxWeightAndDistance(goodsEntities, distance);
        _reportsRepository.CalculateNewMaxVolumeAndDistance(goodsEntities, distance);
        _reportsRepository.CalculateWavgPrice(finalPrice, goods.Count);
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

    public async Task<ReportModel> GetReport(CancellationToken cancellationToken)
    {
        ReportEntity reportEntity =  await _reportsRepository.GetReportData(cancellationToken);
        ReportModel reportModel = await reportEntity.MapEntityToModel();

        return reportModel;
    }
}