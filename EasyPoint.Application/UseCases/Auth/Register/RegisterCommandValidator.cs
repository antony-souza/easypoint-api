using FluentValidation;

namespace EasyPoint.Application.UseCases.Auth.Register;

public sealed class CommandValidator : AbstractValidator<RegisterCommand>
{
    public CommandValidator()
    {
        RuleFor(x => x.StoreId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
