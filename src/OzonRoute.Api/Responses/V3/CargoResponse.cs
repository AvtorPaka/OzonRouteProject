namespace OzonRoute.Api.Responses.V3;
public record CargoResponse (
    long[] GoodsIds,
    double Volume = 0.0,
    double Weight = 0.0
) {}