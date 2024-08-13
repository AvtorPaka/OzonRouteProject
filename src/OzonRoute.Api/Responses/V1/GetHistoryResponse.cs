namespace OzonRoute.Api.Responses.V1;

public record GetHistoryResponse (
    long Id,
    long UserId,
    CargoResponse Cargo,
    decimal Price,
    DateTimeOffset At
) {}