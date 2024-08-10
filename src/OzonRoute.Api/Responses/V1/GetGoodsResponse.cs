namespace OzonRoute.Api.Responses.V1;
public record GetGoodsResponse(
    string Name,
    long Id,
    int Count,
    //In m
    double Length = 0.0d,
    double Width = 0.0d,
    double Height = 0.0d,
    double Weight = 0.0d, //In kg
    decimal Price = 0.0m
)
{ }