using FluentValidation;
using OzonRoute.Domain.Models;

namespace OzonRoute.Domain.Validators;

public class GetHistoryModelValidator: AbstractValidator<GetHistoryModel>
{
    public GetHistoryModelValidator()
    {
        RuleFor(x => x.Take).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
        RuleFor(x => x.UserId).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
    }
}