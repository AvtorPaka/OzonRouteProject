using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Models.Extensions;
public static class GoodModelExtensions
{
    private const double cm3ToM3Ratio = 1000000.0d;
    public static double CalculateVolume(this GoodModel goodModel)
    {
        return goodModel.Lenght * goodModel.Width * goodModel.Height / cm3ToM3Ratio;
    }
}