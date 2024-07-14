using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Requests.V3;

namespace OzonRoute.Api.Responses.V3.Extensions;

public static class GetHistoryResponseExtensions
{   
    private const double mm3ToM3Ratio = 1000000000.0d;
    public static GetHistoryResponse MapModelToResponse(this CalculateLogModel calculateLogModel)
    {
        return new GetHistoryResponse(
            At: calculateLogModel.At,
            Cargo: new CargoResponse(
                Volume: calculateLogModel.Volume / mm3ToM3Ratio,
                Weight: calculateLogModel.Weight),
            Price: calculateLogModel.Price,
            Distance: calculateLogModel.Distance
        );
    }

    public static async Task<List<GetHistoryResponse>> MapModelsToResponses(this List<CalculateLogModel> calculateLogModels)
    {
        return await Task.FromResult(calculateLogModels.Select(m => m.MapModelToResponse()).ToList());
    }
}