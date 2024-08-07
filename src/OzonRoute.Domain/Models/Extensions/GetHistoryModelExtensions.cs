using OzonRoute.Domain.Shared.Data.Models;

namespace OzonRoute.Domain.Models.Extensions;

public static class GetHistoryModelExtensions
{
    public static CalculationHistoryQueryModel MapModelToDalModel(this GetHistoryModel getHistoryModel)
    {
        return new CalculationHistoryQueryModel(
            UserID: getHistoryModel.UserId,
            Limit: getHistoryModel.Take,
            Offset: getHistoryModel.Skip
        );
    }
}