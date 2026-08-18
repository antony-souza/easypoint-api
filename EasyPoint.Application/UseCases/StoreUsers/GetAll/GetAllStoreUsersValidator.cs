using FluentValidation;

namespace EasyPoint.Application.UseCases.StoreUsers.GetAll;

public sealed class GetAllStoreUsersValidator : AbstractValidator<GetAllStoreUsersQuery>
{
    public GetAllStoreUsersValidator()
    {
        RuleFor(query => query.StoreId)
            .NotEmpty();

        RuleFor(query => query.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(query => query.PerPage)
            .InclusiveBetween(1, 100);
    }
}
