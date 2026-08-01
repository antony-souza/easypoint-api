using DotNetEnv;
using EasyPoint.Infrastructure;
using EasyPoint.Application.Common.Abstractions;
using EasyPoint.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<EasyPointDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.Scan(scan => scan
    .FromAssembliesOf(
        typeof(IUseCase),
        typeof(InfrastructureAssemblyMarker))
    .AddClasses(classes => classes.AssignableToAny(
        typeof(IUseCase),
        typeof(IRepository)))
    .AsSelf()
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
