namespace OzonRoute.Api.Responses.V2;

public record GetHistoryResponse (
    long Id,
    long UserId,
    CargoResponse Cargo,
    double Price,
    DateTimeOffset At
) {}