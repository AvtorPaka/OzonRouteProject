using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Dal.Contexts;

internal sealed class DeliveryPriceContext
{
    public List<CalculationEntityV1> Storage { get; init; } = new List<CalculationEntityV1>();
    public ReportEntity Report { get; init; } = new ReportEntity();
}