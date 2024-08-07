using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Infrastructure.Dal.Contexts;

//TODO: Cut that shit off and add redis
internal sealed class DeliveryPriceContext
{
    public ReportEntity Report { get; init; } = new ReportEntity();
}