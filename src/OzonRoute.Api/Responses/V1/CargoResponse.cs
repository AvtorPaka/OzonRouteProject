namespace OzonRoute.Api.Responses.V1;
public record CargoResponse (
    long[] GoodsIds,
    double Volume = 0.0
) {}