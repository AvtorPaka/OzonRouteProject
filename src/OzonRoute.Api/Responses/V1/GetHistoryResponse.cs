namespace OzonRoute.Api.Responses.V1;

public record GetHistoryResponse (
    DateTime At,
    CargoResponse Cargo,
    double Price = 0.0
) {}