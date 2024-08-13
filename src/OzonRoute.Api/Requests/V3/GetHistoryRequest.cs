namespace OzonRoute.Api.Requests.V3;

public record GetHistoryRequest(
    long UserId,
    int Take = 0,
    int Skip = 0
)
{ }
