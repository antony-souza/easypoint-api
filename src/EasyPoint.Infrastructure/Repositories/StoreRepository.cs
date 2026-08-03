using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;

namespace EasyPoint.Infrastructure.Repositories;

public class StoreRepository(EasyPointDbContext context) : Repository<Store>(context), IStoreRepository
{
}