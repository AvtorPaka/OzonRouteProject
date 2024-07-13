namespace OzonRoute.Api.Requests.V2;

public record GoodProperties(
    int Lenght = 0,
    int Width = 0,
    int Height = 0,
    double Weight = 0.0)
{ }