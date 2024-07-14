using OzonRoute.Api.Dal.Models;
using OzonRoute.Api.Dal.Models.Extensions;

namespace OzonRoute.Api.Bll.Models.Extensions;
public static class ReportModelExtensions
{
    public static async Task<ReportModel> MapEntityToModel(this ReportEntity reportEntity)
    {
        return await Task.FromResult(new ReportModel(
            MaxWeight: reportEntity.MaxWeight,
            MaxVolume: reportEntity.MaxVolume,
            MaxDistanceForHeaviestGood: reportEntity.MaxDistanceForHeaviestGood,
            MaxDistanceForLargestGood: reportEntity.MaxDistanceForLargestGood,
            WavgPrice: reportEntity.GetWavgPrice()
        ));
    }
}