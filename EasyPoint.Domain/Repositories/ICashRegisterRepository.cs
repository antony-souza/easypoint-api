using EasyPoint.Domain.Entities.CashRegisters;

namespace EasyPoint.Domain.Repositories;

public interface ICashRegisterRepository : IRepository<CashRegister>
{
    Task<(IReadOnlyList<CashRegister> CashRegisters, int TotalItems)> GetPagedByStoreAsync(
        Guid storeId,
        int skip,
        int take,
        CancellationToken cancellationToken = default);

    Task<CashRegister?> GetByCodeAsync(
        Guid storeId,
        string code,
        CancellationToken cancellationToken = default);
}
