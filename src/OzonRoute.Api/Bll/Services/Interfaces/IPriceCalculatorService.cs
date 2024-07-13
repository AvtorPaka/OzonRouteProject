using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    public double CalculatePrice(List<GoodModel> goods);
    public Task<List<CalculateLogModel>> QueryLog(int take);
}