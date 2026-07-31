using DotNetEnv;
using EasyPoint.Application.Common.Abstractions;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Scan(scan => scan
    .FromAssemblyOf<IUseCase>()
    .AddClasses(classes => classes.AssignableTo<IUseCase>())
    .AsSelf()
    .AsImplementedInterfaces()
    .WithScopedLifetime());
    
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
