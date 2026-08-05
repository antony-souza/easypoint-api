using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.CashRegisters;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.CashRegisters.Create;

public sealed class CreateCashRegisterHandler(
    ICashRegisterRepository cashRegisterRepository,
    ICurrentUser currentUser)
    : IRequestHandler<CreateCashRegisterCommand, Result<CreateCashRegisterResponse>>
{
    public async Task<Result<CreateCashRegisterResponse>> Handle(
        CreateCashRegisterCommand request,
        CancellationToken cancellationToken)
    {
        var normalizedCode = request.Code.Trim().ToUpperInvariant();

        var existingCashRegister = await cashRegisterRepository.GetByCodeAsync(
            currentUser.StoreId,
            normalizedCode,
            cancellationToken);

        if (existingCashRegister is not null)
        {
            return Result<CreateCashRegisterResponse>.Failure(
                "Já existe um caixa com este código nesta loja.");
        }

        var cashRegister = new CashRegister
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Code = normalizedCode,
            IsActive = true,
            StoreId = currentUser.StoreId
        };

        var createdCashRegister = await cashRegisterRepository.CreateAsync(
            cashRegister,
            cancellationToken);

        return Result<CreateCashRegisterResponse>.Success(
            new CreateCashRegisterResponse(
                Id: createdCashRegister.Id,
                Name: createdCashRegister.Name,
                Code: createdCashRegister.Code,
                IsActive: createdCashRegister.IsActive,
                StoreId: createdCashRegister.StoreId));
    }
}
