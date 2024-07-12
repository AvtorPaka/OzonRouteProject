using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Bll.Services.Interfaces;

public interface IPriceCalculator
{
    public Task<double> CalculatePrice(GoodModel[] goods);
    public Task<CalculateLogModel[]> QueryLog(int take);
}