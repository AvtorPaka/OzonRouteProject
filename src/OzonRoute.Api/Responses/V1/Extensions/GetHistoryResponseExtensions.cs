using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Responses.V1.Extensions;

public static class GetHistoryResponseExtensions
{   
    private const double cm3ToMm3Ratio = 1000.0d;
    public static GetHistoryResponse MapModelToResponse(this CalculateLogModel calculateLogModel)
    {
        return new GetHistoryResponse(
            At: calculateLogModel.At,
            Cargo: new CargoResponse(calculateLogModel.Volume * cm3ToMm3Ratio),
            Price: calculateLogModel.Price
        );
    }

    public static async Task<IReadOnlyList<GetHistoryResponse>> MapModelsToResponses(this IReadOnlyList<CalculateLogModel> calculateLogModels)
    {
        return await Task.FromResult(calculateLogModels.Select(m => m.MapModelToResponse()).ToList());
    }
}