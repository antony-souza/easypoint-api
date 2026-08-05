using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Pagination;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.CashRegisters.GetAll;

public sealed class GetCashRegistersHandler(
    ICashRegisterRepository cashRegisterRepository,
    ICurrentUser currentUser)
    : IRequestHandler<
        GetCashRegistersQuery,
        Result<PagedResponse<GetCashRegistersItemResponse>>>
{
    public async Task<Result<PagedResponse<GetCashRegistersItemResponse>>> Handle(
        GetCashRegistersQuery request,
        CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * request.PerPage;

        var (cashRegisters, totalItems) =
            await cashRegisterRepository.GetPagedByStoreAsync(
                currentUser.StoreId,
                skip,
                request.PerPage,
                cancellationToken);

        var items = cashRegisters
            .Select(cashRegister => new GetCashRegistersItemResponse(
                Id: cashRegister.Id,
                Name: cashRegister.Name,
                Code: cashRegister.Code,
                IsActive: cashRegister.IsActive))
            .ToList();

        return Result<PagedResponse<GetCashRegistersItemResponse>>.Success(
            new PagedResponse<GetCashRegistersItemResponse>(
                items,
                request.Page,
                request.PerPage,
                totalItems));
    }
}
