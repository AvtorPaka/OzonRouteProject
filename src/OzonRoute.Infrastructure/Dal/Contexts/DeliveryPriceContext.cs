using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Dal.Contexts;

internal sealed class DeliveryPriceContext
{
    public List<GoodPriceEntity> Storage { get; init; } = new List<GoodPriceEntity>();
    public ReportEntity Report { get; init; } = new ReportEntity();
}