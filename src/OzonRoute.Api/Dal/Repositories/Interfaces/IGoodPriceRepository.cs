using OzonRoute.Api.Dal.Models;

namespace OzonRoute.Api.Dal.Repositories.Interfaces;

public interface IGoodPriceRepository
{
    public void Save(GoodPriceEntity goodPriceData);
    public Task<List<GoodPriceEntity>> QueryData();
}