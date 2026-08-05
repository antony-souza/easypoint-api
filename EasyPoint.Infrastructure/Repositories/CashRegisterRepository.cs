using EasyPoint.Domain.Entities.CashRegisters;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Repositories;

public sealed class CashRegisterRepository(EasyPointDbContext context)
    : Repository<CashRegister>(context), ICashRegisterRepository
{
    public async Task<(IReadOnlyList<CashRegister> CashRegisters, int TotalItems)> GetPagedByStoreAsync(
        Guid storeId,
        int skip,
        int take,
        CancellationToken cancellationToken = default)
    {
        var query = context.CashRegisters
            .AsNoTracking()
            .Where(cashRegister => cashRegister.StoreId == storeId)
            .OrderBy(cashRegister => cashRegister.Name)
            .ThenBy(cashRegister => cashRegister.Code);

        var totalItems = await query.CountAsync(cancellationToken);
        var cashRegisters = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return (cashRegisters, totalItems);
    }

    public Task<CashRegister?> GetByCodeAsync(
        Guid storeId,
        string code,
        CancellationToken cancellationToken = default)
    {
        return context.CashRegisters.SingleOrDefaultAsync(
            cashRegister =>
                cashRegister.StoreId == storeId &&
                cashRegister.Code == code,
            cancellationToken);
    }
}
