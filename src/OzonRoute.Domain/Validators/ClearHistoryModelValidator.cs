using FluentValidation;
using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Validators;

public class ClearHistoryModelValidator : AbstractValidator<ClearHistoryModel>
{
    public ClearHistoryModelValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleForEach(x => x.CalculationIds).NotNull();
    }
}