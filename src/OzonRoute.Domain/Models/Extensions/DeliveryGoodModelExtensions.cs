using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Models.Extensions;
public static class DeliveryGoodModelExtensions
{
    private const double cm3ToM3Ratio = 1000000.0d;
    public static double CalculateVolume(this DeliveryGoodModel goodModel)
    {
        return goodModel.Lenght * goodModel.Width * goodModel.Height / cm3ToM3Ratio;
    }

    public static CalculationGoodEntityV1 MapModelToEntity(this DeliveryGoodModel deliveryGoodModel, long userId)
    {
        return new CalculationGoodEntityV1{
            Id = -1,
            UserId = userId,
            Width = deliveryGoodModel.Width,
            Height = deliveryGoodModel.Height,
            Length =  deliveryGoodModel.Lenght,
            Weight = deliveryGoodModel.Weight
        };
    }

    public static CalculationGoodEntityV1[] MapModelsToEntities(this IEnumerable<DeliveryGoodModel> deliveryGoodModels, long user_id)
    {
        return deliveryGoodModels.Select(x => x.MapModelToEntity(user_id)).ToArray();
    }
}