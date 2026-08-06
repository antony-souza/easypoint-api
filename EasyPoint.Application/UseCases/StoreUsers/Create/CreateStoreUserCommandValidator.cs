using FluentValidation;

namespace EasyPoint.Application.UseCases.StoreUsers.Create;

public sealed class CreateStoreUserCommandValidator
    : AbstractValidator<CreateStoreUserCommand>
{
    public CreateStoreUserCommandValidator()
    {
        RuleFor(command => command.StoreId)
            .NotEmpty();

        RuleFor(command => command.UserId)
            .NotEmpty();
    }
}
