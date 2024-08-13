using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;
using OzonRoute.Domain.Services.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.Services;

internal sealed class ReportsService : IReportsService
{   
    private readonly IReportsRepository _reportsRepository;

    public ReportsService(IReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
    }

    public async Task UpdateReportData(SaveCalculationModel saveModel, CancellationToken cancellationToken)
    {   
        var tasks = new List<Task>()
        {
            UpdatePersonalReportData(saveModel, cancellationToken),
            UpdateGlobalReportData(saveModel, cancellationToken)
        };

        await TaskExt.WhenAll(tasks);
    }

    private async Task UpdatePersonalReportData(SaveCalculationModel saveModel, CancellationToken cancellationToken)
    {
        long userId = saveModel.GoodsContainer.UserId;

        var reportModel = (await _reportsRepository.Get(
            userId: userId,
            cancellationToken: cancellationToken
        )).MapEntityToModel(userId);

        UpdateMaxWeightAndVolume(
            model: reportModel,
            goods: saveModel.GoodsContainer.Goods,
            distance: saveModel.GoodsContainer.Distance
        );

        UpdateWavgPrice(
            model: reportModel,
            addPrice: (double)saveModel.Price,
            addCount: saveModel.GoodsContainer.Goods.Count
        );

        await _reportsRepository.AddOrUpdate(
            entity: reportModel.MapModelToEntity(),
            cancellationToken: cancellationToken
        );
    }

    private async Task UpdateGlobalReportData(SaveCalculationModel saveModel ,CancellationToken cancellationToken)
    {
        var reportModel = (await _reportsRepository.Get(
            userId: -1,
            cancellationToken: cancellationToken
        )).MapEntityToModel(-1);

        UpdateMaxWeightAndVolume(
            model: reportModel,
            goods: saveModel.GoodsContainer.Goods,
            distance: saveModel.GoodsContainer.Distance
        );

        UpdateWavgPrice(
            model: reportModel,
            addPrice: (double)saveModel.Price,
            addCount: saveModel.GoodsContainer.Goods.Count
        );

        await _reportsRepository.AddOrUpdate(
            entity: reportModel.MapModelToEntity(),
            cancellationToken: cancellationToken
        );
    }

    private static void UpdateMaxWeightAndVolume(ReportModel model, IEnumerable<DeliveryGoodModel> goods, int distance)
    {
        double maxWeight = goods.Max(x => x.Weight);
        if (maxWeight >= model.MaxWeight)
        {
            model.MaxWeight = maxWeight;
            model.MaxDistanceForHeaviestGood = Math.Max(model.MaxDistanceForHeaviestGood, distance);
        }

        double maxVolume = goods.Max(x => x.CalculateVolume());
        if (maxVolume >= model.MaxVolume)
        {
            model.MaxVolume = maxVolume;
            model.MaxDistanceForLargestGood = Math.Max(model.MaxDistanceForLargestGood, distance);
        }
    }

    private static void UpdateWavgPrice(ReportModel model, double addPrice, int addCount)
    {
        model.SummaryPrice += addPrice;
        model.TotalNumberOfGoods += addCount;
    }

    public async Task<ReportModel> GetReport(long userId, CancellationToken cancellationToken)
    {
        var reportEntity = await _reportsRepository.Get(userId, cancellationToken);
        return reportEntity.MapEntityToModel(userId); 
    }

    public async Task ClearReportData(long userId, CancellationToken cancellationToken)
    {
        await _reportsRepository.Delete(userId, cancellationToken);
    }
}