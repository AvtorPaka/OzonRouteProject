using FluentValidation;
using OzonRoute.Api.Requests.V2;

namespace OzonRoute.Api.Validators.V2;
public class GetHistoryRequestValidator: AbstractValidator<GetHistoryRequest>
{
    public GetHistoryRequestValidator()
    {
        RuleFor(r => r.Take).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
    }
}
