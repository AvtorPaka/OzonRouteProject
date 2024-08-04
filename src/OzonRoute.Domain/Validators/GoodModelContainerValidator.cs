using FluentValidation;
using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Validators;
public class GoodModelsContainerValidator : AbstractValidator<GoodModelsContainer>
{
    public GoodModelsContainerValidator()
    {
        RuleFor(x => x.Goods).NotEmpty().NotNull();
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Distance).GreaterThan(1).LessThanOrEqualTo(int.MaxValue);
        RuleForEach(x => x.Goods).ChildRules(good =>
        {
            good.RuleFor(x => x.Lenght).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Height).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Width).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            good.RuleFor(x => x.Weight).GreaterThanOrEqualTo(0).LessThanOrEqualTo(int.MaxValue);
        });
    }
}