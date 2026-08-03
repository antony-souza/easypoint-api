using DotNetEnv;
using EasyPoint.Application;
using EasyPoint.Infrastructure;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<EasyPointDbContext>(options => { options.UseNpgsql(connectionString); });

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
