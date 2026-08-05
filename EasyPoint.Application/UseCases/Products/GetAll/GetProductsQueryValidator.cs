using FluentValidation;

namespace EasyPoint.Application.UseCases.Products.GetAll;

public sealed class GetProductsQueryValidator
    : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(1_000_000);

        RuleFor(x => x.PerPage)
            .InclusiveBetween(1, 15);
    }
}
