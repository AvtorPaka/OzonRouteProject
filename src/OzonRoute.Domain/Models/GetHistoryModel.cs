namespace OzonRoute.Domain.Models;
public record GetHistoryModel(
    long UserId,
    int Take,
    int Skip
) {}