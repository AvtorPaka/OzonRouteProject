using FluentValidation;
using OzonRoute.Api.Requests.V3;

namespace OzonRoute.Api.Validators.V3;
public class GetHistoryRequestValidator: AbstractValidator<GetHistoryRequest>
{
    public GetHistoryRequestValidator()
    {
        RuleFor(r => r.Take).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
    }
}
