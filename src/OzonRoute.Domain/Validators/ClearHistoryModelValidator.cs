using FluentValidation;
using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Validators;

public class ClearHistoryModelValidator : AbstractValidator<ClearHistoryModel>
{
    public ClearHistoryModelValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.CalculationIds).NotNull();
        RuleForEach(x => x.CalculationIds).ChildRules(num => 
        {
            num.RuleFor(x => x).GreaterThan(0);
        });
    }
}