using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPoint.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}