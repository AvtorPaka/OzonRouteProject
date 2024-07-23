using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Responses.V1.Extensions;

public static class ReportsResponseExtensions
{
    public static async Task<ReportsResponse> MapModelToResponse(this ReportModel reportModel)
    {
        return await Task.FromResult(new ReportsResponse(
            MaxWeight: reportModel.MaxWeight,
            MaxVolume: reportModel.MaxVolume,
            MaxDistanceForHeaviestGood: reportModel.MaxDistanceForHeaviestGood,
            MaxDistanceForLargestGood: reportModel.MaxDistanceForLargestGood,
            WavgPrice: reportModel.WavgPrice
        ));
    }
}