using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Requests.V1;

namespace OzonRoute.Api.Responses.V1.Extensions;

public static class GetHistoryResponseExtensions
{   
    public static GetHistoryResponse MapModelToResponse(this CalculateLogModel calculateLogModel)
    {
        return new GetHistoryResponse(
            At: calculateLogModel.At,
            Cargo: new CargoResponse(calculateLogModel.Volume),
            Price: calculateLogModel.Price
        );
    }

    public static async Task<List<GetHistoryResponse>> MapModelsToResponses(this List<CalculateLogModel> calculateLogModels)
    {
        return await Task.FromResult(calculateLogModels.Select(m => m.MapModelToResponse()).ToList());
    }
}