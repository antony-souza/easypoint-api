using FluentValidation;

namespace EasyPoint.Application.UseCases.CashRegisters.GetAll;

public sealed class GetCashRegistersQueryValidator
    : AbstractValidator<GetCashRegistersQuery>
{
    public GetCashRegistersQueryValidator()
    {
        RuleFor(query => query.StoreId)
            .NotEmpty();

        RuleFor(query => query.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(query => query.PerPage)
            .InclusiveBetween(1, 100);
    }
}
