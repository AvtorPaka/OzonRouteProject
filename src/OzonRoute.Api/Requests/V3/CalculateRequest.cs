namespace OzonRoute.Api.Requests.V3;

public record CalculateRequest(
    List<GoodProperties> Goods,
    int Distance = 0) {}