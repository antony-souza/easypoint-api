using FluentValidation;

namespace EasyPoint.Application.UseCases.Categories.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(50);

        RuleFor(x => x.StoreId)
            .NotEmpty();
    }
}