namespace OzonRoute.Api.Dal.Models.Extensions;
public static class ReportEntityExtensions
{
    public static double GetWavgPrice(this ReportEntity reportEntity)
    {
        return reportEntity.TotalNumberOfGoods == 0 ?
                0.0 : (reportEntity.SummaryPrice / reportEntity.TotalNumberOfGoods);
    }
}