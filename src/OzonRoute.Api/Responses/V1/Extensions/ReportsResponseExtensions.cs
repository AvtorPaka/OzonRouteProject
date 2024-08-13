using OzonRoute.Domain.Models;
using OzonRoute.Domain.Models.Extensions;

namespace OzonRoute.Api.Responses.V1.Extensions;

public static class ReportsResponseExtensions
{
    public static ReportsResponse MapModelToResponse(this ReportModel reportModel)
    {
        return new ReportsResponse(
            MaxWeight: reportModel.MaxWeight,
            MaxVolume: reportModel.MaxVolume,
            MaxDistanceForHeaviestGood: reportModel.MaxDistanceForHeaviestGood,
            MaxDistanceForLargestGood: reportModel.MaxDistanceForLargestGood,
            TotalNumberOfGoods: reportModel.TotalNumberOfGoods,
            SummaryPrice: reportModel.SummaryPrice,
            WavgPrice: reportModel.GetWavgPrice()
        );
    }
}