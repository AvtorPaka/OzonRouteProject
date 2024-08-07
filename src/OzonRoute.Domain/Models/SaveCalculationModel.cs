namespace OzonRoute.Domain.Models;

public record SaveCalculationModel(
    DeliveryGoodsContainer GoodsContainer,
    double TotalVolume,
    double TotalWeight,
    decimal Price,
    DateTimeOffset At
) {}