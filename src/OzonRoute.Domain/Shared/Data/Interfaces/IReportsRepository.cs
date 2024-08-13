
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Domain.Shared.Data.Interfaces;

public interface IReportsRepository
{
    public Task<ReportEntity> Get(long userId, CancellationToken cancellationToken);
    public Task AddOrUpdate(ReportEntity entity, CancellationToken cancellationToken);
    public Task AddOrUpdateWithExpiration(ReportEntity entity, CancellationToken cancellationToken);
    public Task Delete(long userId, CancellationToken cancellationToken);
}