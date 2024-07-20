using FluentValidation;
using OzonRoute.Api.Requests.V1;

namespace OzonRoute.Api.Validators.V1;
public class GetHistoryRequestValidator: AbstractValidator<GetHistoryRequest>
{
    public GetHistoryRequestValidator()
    {
        RuleFor(r => r.Take).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
    }
}
