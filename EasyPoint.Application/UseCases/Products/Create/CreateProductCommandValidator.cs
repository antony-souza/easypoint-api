using FluentValidation;

namespace EasyPoint.Application.UseCases.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.BarCode)
            .MinimumLength(1)
            .MaximumLength(14)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .NotNull();
    }
}