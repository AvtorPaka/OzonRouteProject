using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Entities.Extensions;
public static class ReportEntityExtensions
{
    public static double GetWavgPrice(this ReportEntity reportEntity)
    {
        return reportEntity.TotalNumberOfGoods == 0 ?
                0.0 : reportEntity.SummaryPrice / reportEntity.TotalNumberOfGoods;
    }
}