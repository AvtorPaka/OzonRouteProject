using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Repositories.Interfaces;

public interface IGoodPriceRepository
{
    public Task Save(GoodPriceEntity goodPriceData);
    public Task<GoodPriceEntity> QueryData();
}