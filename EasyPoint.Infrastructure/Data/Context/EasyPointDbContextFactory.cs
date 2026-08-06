using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EasyPoint.Infrastructure.Data.Context;

public sealed class EasyPointDbContextFactory
    : IDesignTimeDbContextFactory<EasyPointDbContext>
{
    public EasyPointDbContext CreateDbContext(string[] args)
    {
        Env.TraversePath().Load();

        var connectionString = Environment.GetEnvironmentVariable(
            "ConnectionStrings__Default");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "ConnectionStrings__Default was not found in the environment.");
        }

        var options = new DbContextOptionsBuilder<EasyPointDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new EasyPointDbContext(options);
    }
}
