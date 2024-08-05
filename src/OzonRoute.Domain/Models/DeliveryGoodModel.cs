namespace OzonRoute.Domain.Models;

public record DeliveryGoodModel(
    //In cm
    double Lenght = 0.0d,
    double Width = 0.0d,
    double Height = 0.0d,
    //In kg
    double Weight = 0.0d
)
{ }
