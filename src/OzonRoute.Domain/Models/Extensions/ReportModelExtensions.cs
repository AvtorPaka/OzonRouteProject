using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Models.Extensions;
public static class ReportModelExtensions
{
    public static ReportModel MapEntityToModel(this ReportEntity reportEntity, long userId)
    {
        return new ReportModel(
            userId: userId,
            maxWeight: reportEntity.MaxWeight,
            maxVolume: reportEntity.MaxVolume,
            maxDistanceForHeaviestGood: reportEntity.MaxDistanceForHeaviestGood,
            maxDistanceForLargestGood: reportEntity.MaxDistanceForLargestGood,
            totalNumberOfGoods: reportEntity.TotalNumberOfGoods,
            summaryPrice: reportEntity.SummaryPrice
        );
    }

    public static ReportEntity MapModelToEntity(this ReportModel model)
    {
        return new ReportEntity()
        {
            UserId = model.UserId,
            MaxWeight = model.MaxWeight,
            MaxVolume = model.MaxVolume,
            MaxDistanceForLargestGood = model.MaxDistanceForLargestGood,
            MaxDistanceForHeaviestGood = model.MaxDistanceForHeaviestGood,
            TotalNumberOfGoods = model.TotalNumberOfGoods,
            SummaryPrice = model.SummaryPrice
        };
    }

    public static double GetWavgPrice(this ReportModel model)
    {
        return model.TotalNumberOfGoods == 0 ? 0 :
                                                model.SummaryPrice / model.TotalNumberOfGoods;
    }
}