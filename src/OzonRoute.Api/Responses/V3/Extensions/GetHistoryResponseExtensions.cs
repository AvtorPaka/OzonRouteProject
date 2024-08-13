using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Responses.V3.Extensions;

public static class GetHistoryResponseExtensions
{   
    private const double cm3ToM3Ratio = 1000000.0d;
    public static GetHistoryResponse MapModelToResponse(this CalculationLogModel calculationLogModel)
    {
        return new GetHistoryResponse(
            At: calculationLogModel.At,
            Id: calculationLogModel.Id,
            UserId: calculationLogModel.UserId,
            Cargo: new CargoResponse(
                GoodsIds: calculationLogModel.GoodsIds,
                Volume: calculationLogModel.TotalVolume / cm3ToM3Ratio,
                Weight: calculationLogModel.TotalWeight),
            Price: calculationLogModel.Price,
            Distance: calculationLogModel.Distance
        );
    }

    public static async Task<IReadOnlyList<GetHistoryResponse>> MapModelsToResponses(this IReadOnlyList<CalculationLogModel> calculationLogModels)
    {
        return await Task.FromResult(calculationLogModels.Select(m => m.MapModelToResponse()).ToList());
    }
}