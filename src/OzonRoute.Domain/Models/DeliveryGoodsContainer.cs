namespace OzonRoute.Domain.Models;

public record DeliveryGoodsContainer(
    long UserId,
    IReadOnlyList<DeliveryGoodModel> Goods,
    int Distance
) {}