using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculator
{
    public async Task<double> CalculatePrice(GoodModel[] goods)
    {   
        await Task.Delay(TimeSpan.FromSeconds(10));
        throw new NotImplementedException();
    }

    public async Task<CalculateLogModel[]> QueryLog(int take)
    {   
        await Task.Delay(TimeSpan.FromSeconds(10));
        throw new NotImplementedException();
    }
}