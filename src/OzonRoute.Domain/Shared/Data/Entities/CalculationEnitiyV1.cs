using System.Security.Cryptography.X509Certificates;

namespace OzonRoute.Domain.Shared.Data.Entities;

public class CalculationEntityV1
{
    public long Id {get; init;}
    public long UserId {get; init;}
    public long[] GoodIds {get; init;} = Array.Empty<long>();
    public double TotalVolume {get; init;}//In cm^3
    public double TotalWeight {get; init;} //In gramms
    public double Price {get; init;}
    public double Distance {get; init;} //In metrs
    public DateTimeOffset At {get; init;}
}