using FluentValidation;

namespace EasyPoint.Application.UseCases.CashRegisters.Create;

public sealed class CreateCashRegisterCommandValidator
    : AbstractValidator<CreateCashRegisterCommand>
{
    public CreateCashRegisterCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Code)
            .NotEmpty()
            .MaximumLength(50);
    }
}
