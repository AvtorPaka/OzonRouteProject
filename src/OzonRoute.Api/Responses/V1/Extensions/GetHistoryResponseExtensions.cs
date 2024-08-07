using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Responses.V1.Extensions;

public static class GetHistoryResponseExtensions
{   
    private const double cm3ToMm3Ratio = 1000.0d;
    public static GetHistoryResponse MapModelToResponse(this CalculationLogModel calculationLogModel)
    {
        return new GetHistoryResponse(
            At: calculationLogModel.At,
            Id: calculationLogModel.Id,
            UserId: calculationLogModel.UserId,
            Cargo: new CargoResponse(
                Volume: calculationLogModel.TotalVolume * cm3ToMm3Ratio,
                GoodsIds: calculationLogModel.GoodsIds),
            Price: calculationLogModel.Price
        );
    }

    public static async Task<IReadOnlyList<GetHistoryResponse>> MapModelsToResponses(this IReadOnlyList<CalculationLogModel> calculationLogModels)
    {
        return await Task.FromResult(calculationLogModels.Select(m => m.MapModelToResponse()).ToList());
    }
}