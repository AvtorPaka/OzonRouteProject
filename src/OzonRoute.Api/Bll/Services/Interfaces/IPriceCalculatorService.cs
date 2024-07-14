using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    public double CalculatePrice(List<GoodModel> goods, double distance);
    public Task<List<CalculateLogModel>> QueryLog(int take);
}