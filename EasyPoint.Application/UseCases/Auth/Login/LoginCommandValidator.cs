using FluentValidation;

namespace EasyPoint.Application.UseCases.Auth.Login;

public sealed class CommandValidator : AbstractValidator<LoginCommand>
{
    public CommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
