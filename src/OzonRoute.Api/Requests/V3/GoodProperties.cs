namespace OzonRoute.Api.Requests.V3;

public record GoodProperties(
    int Lenght = 0,
    int Width = 0,
    int Height = 0,
    double Weight = 0.0)
{ }