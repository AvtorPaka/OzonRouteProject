namespace OzonRoute.Domain.Models;

public record GoodModelsContainer(
    long UserId,
    IReadOnlyList<GoodModel> Goods,
    int Distance
) {}