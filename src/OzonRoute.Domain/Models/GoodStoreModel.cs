namespace OzonRoute.Domain.Models;

public record GoodStoreModel(
    string Name,
    int Id,
    int Count,
    //In cm
    double Lenght = 0.0d,
    double Width = 0.0d,
    double Height = 0.0d,
    double Weight = 0.0d, //In kg
    double Price = 0.0d
)
{ }