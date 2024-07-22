using FluentValidation;
using OzonRoute.Api.Requests.V1;

namespace OzonRoute.Api.Validators.V1;
public class CalculateRequestValidator : AbstractValidator<CalculateRequest>
{
    public CalculateRequestValidator()
    {
        RuleFor(x => x.Goods).NotEmpty().NotNull();
        RuleForEach(x => x.Goods).ChildRules(good =>
        {
            good.RuleFor(x => x.Lenght).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Height).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Width).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
        });
    }
}