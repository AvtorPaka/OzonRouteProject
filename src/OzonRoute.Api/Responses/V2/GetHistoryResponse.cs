namespace OzonRoute.Api.Responses.V2;

public record GetHistoryResponse (
    DateTime At,
    CargoResponse Cargo,
    double Price = 0.0
) {}