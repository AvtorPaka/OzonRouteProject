using System.Security.Cryptography.X509Certificates;

namespace OzonRoute.Domain.Shared.Data.Entities;

public class CalculationEntityV1
{
    public CalculationEntityV1(long id, long userId, long[] goodIds, double totalVolume, double totalWeight, double price, double distance, DateTime at)
    {
        Id = id;
        UserId = userId;
        GoodIds = goodIds;
        TotalVolume = totalVolume;
        TotalWeight = totalWeight;
        Price = price;
        Distance = distance;
        At = at;
    }

    public long Id {get; init;}
    public long UserId {get; init;}
    public long[] GoodIds {get; init;} = Array.Empty<long>();
    public double TotalVolume {get; init;}//In cm^3
    public double TotalWeight {get; init;} //In gramms
    public double Price {get; init;}
    public double Distance {get; init;} //In metrs
    public DateTime At {get; init;}
}