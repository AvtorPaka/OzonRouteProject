namespace OzonRoute.Api.Requests.V2;

public record CalculateRequest(
    long UserId,
    List<GoodProperties> Goods) {}