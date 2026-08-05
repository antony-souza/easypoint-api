using EasyPoint.Application.Common.Pagination;
using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.CashRegisters.GetAll;

public sealed record GetCashRegistersQuery(
    int Page = 1,
    int PerPage = 10
) : IRequest<Result<PagedResponse<GetCashRegistersItemResponse>>>;
