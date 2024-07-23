using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Entities.Extensions;

namespace OzonRoute.Domain.Models.Extensions;
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