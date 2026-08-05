using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Repositories;

public class StoreRepository(EasyPointDbContext context) : Repository<Store>(context), IStoreRepository
{
    public Task<Store?> GetByCnpjAsync(
        string cnpj,
        CancellationToken cancellationToken = default)
    {
        var digits = new string(cnpj.Where(char.IsDigit).ToArray());
        var formatted = $"{digits[..2]}.{digits.Substring(2, 3)}.{digits.Substring(5, 3)}/{digits.Substring(8, 4)}-{digits.Substring(12, 2)}";

        return context.Stores.SingleOrDefaultAsync(
            store => store.Cnpj == digits || store.Cnpj == formatted,
            cancellationToken);
    }
}
