using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Requests.V2;

namespace OzonRoute.Api.Responses.V2.Extensions;

public static class GetHistoryResponseExtensions
{   
    private const double kgToGramsRatio = 1000.0d;
    private const double cm3ToMm3Ratio = 1000.0d;
    public static GetHistoryResponse MapModelToResponse(this CalculateLogModel calculateLogModel)
    {
        return new GetHistoryResponse(
            At: calculateLogModel.At,
            Cargo: new CargoResponse(
                Volume: calculateLogModel.Volume * cm3ToMm3Ratio,
                Weight: calculateLogModel.Weight * kgToGramsRatio),
            Price: calculateLogModel.Price
        );
    }

    public static async Task<List<GetHistoryResponse>> MapModelsToResponses(this List<CalculateLogModel> calculateLogModels)
    {
        return await Task.FromResult(calculateLogModels.Select(m => m.MapModelToResponse()).ToList());
    }
}