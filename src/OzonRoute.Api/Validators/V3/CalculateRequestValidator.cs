using FluentValidation;
using OzonRoute.Api.Requests.V3;

namespace OzonRoute.Api.Validators.V3;
public class CalculateRequestValidator : AbstractValidator<CalculateRequest>
{
    public CalculateRequestValidator()
    {
        RuleFor(x => x.Goods).NotEmpty().NotNull();
        RuleFor(x => x.Distance).GreaterThan(1).LessThanOrEqualTo(int.MaxValue);
        RuleForEach(x => x.Goods).ChildRules(good =>
        {
            good.RuleFor(x => x.Lenght).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Height).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Width).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Weight).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
        });
    }
}