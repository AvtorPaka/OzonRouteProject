using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Requests.V1.Extensions;

public static class ClearHisotryRequestExtensions
{
    public static ClearHistoryModel MapRequestToModel(this ClearHistoryRequest request)
    {
        return new ClearHistoryModel(
            UserId: request.UserId,
            CalculationIds: request.CalculationIds ?? new List<long>()
        );
    }
}