namespace OzonRoute.Api.Dal.Repositories.Interfaces;

public interface IReportsRepository
{   
    public void GetReportData();
    public void CalculateNewMaxWeight(double goodsSummaryWeight, double goodsShipDistance);
    public void CalculateNewMaxVolume(double goodsSummaryVolume, double goodsShipDistance);
    public void CalculateWavgPrice(double goodsFinalPrice, int goodsCount);
}