using DotNetEnv;
using EasyPoint.Application;
using EasyPoint.Api.ExceptionHandling;
using EasyPoint.Infrastructure;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<EasyPointDbContext>(options => { options.UseNpgsql(connectionString); });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
