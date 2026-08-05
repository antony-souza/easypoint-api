using FluentValidation;

namespace EasyPoint.Application.UseCases.Stores.Create;

public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Cnpj)
            .MinimumLength(14)
            .MaximumLength(18)
            .NotEmpty()
            .NotNull();
    }
}