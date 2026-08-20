using FluentValidation;

namespace EasyPoint.Application.UseCases.Users.Update;

public sealed class UpdateUsersValitador : AbstractValidator<UpdateUsersCommand>
{
    public UpdateUsersValitador()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(150);
        RuleFor(v => v.Username)
            .NotEmpty().WithMessage("Nome de usuário é obrigatório.")
            .MaximumLength(256);
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.")
            .MaximumLength(256);
    }
}
