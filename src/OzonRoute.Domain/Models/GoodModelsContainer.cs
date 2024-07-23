namespace OzonRoute.Domain.Models;

public record GoodModelsContainer(
    IReadOnlyList<GoodModel> Goods,
    int Distance
) {}