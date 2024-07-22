using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Responses.V3.Extensions;

public static class GetHistoryResponseExtensions
{   
    private const double cm3ToM3Ratio = 1000000.0d;
    public static GetHistoryResponse MapModelToResponse(this CalculateLogModel calculateLogModel)
    {
        return new GetHistoryResponse(
            At: calculateLogModel.At,
            Cargo: new CargoResponse(
                Volume: calculateLogModel.Volume / cm3ToM3Ratio,
                Weight: calculateLogModel.Weight),
            Price: calculateLogModel.Price,
            Distance: calculateLogModel.Distance
        );
    }

    public static async Task<IReadOnlyList<GetHistoryResponse>> MapModelsToResponses(this IReadOnlyList<CalculateLogModel> calculateLogModels)
    {
        return await Task.FromResult(calculateLogModels.Select(m => m.MapModelToResponse()).ToList());
    }
}