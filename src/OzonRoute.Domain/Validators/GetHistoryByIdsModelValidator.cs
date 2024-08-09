using FluentValidation;
using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Validators;

public class GetHistoryByIdsModelValidator : AbstractValidator<GetHistoryByIdsModel>
{
    public GetHistoryByIdsModelValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.CalculationIds).NotNull().NotEmpty();
        RuleForEach(x => x.CalculationIds).ChildRules(num => 
        {
            num.RuleFor(x => x).GreaterThan(0);
        });
    }
}