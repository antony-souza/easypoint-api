using FluentValidation;

namespace EasyPoint.Application.UseCases.Organizations.Create;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(command => command.Cnpj)
            .MinimumLength(14)
            .MaximumLength(18)
            .NotEmpty()
            .NotNull();
    }
}