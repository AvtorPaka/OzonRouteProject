namespace OzonRoute.Api.Responses.V1;

public record GetHistoryResponse (
    GetHistoryProperties[] Logs
) {}