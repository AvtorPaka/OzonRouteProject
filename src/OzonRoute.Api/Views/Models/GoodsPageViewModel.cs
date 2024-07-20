using OzonRoute.Api.Responses.V1;

namespace OzonRoute.Api.Views.Models;

public record GoodsPageViewModel(
    IEnumerable<GetGoodsResponse> Goods = default!
) {}