namespace OzonRoute.Domain.Models;

public record StorageGoodModel(
    string Name,
    long Id,
    int Count,
    //In cm
    double Length = 0.0d,
    double Width = 0.0d,
    double Height = 0.0d,
    double Weight = 0.0d, //In kg
    decimal Price = 0.0m
)
{ }