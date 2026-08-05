using EasyPoint.Application.Common.Behaviors;
using EasyPoint.Application.UseCases.Auth.Login;
using EasyPoint.Application.UseCases.Categories.Create;
using EasyPoint.Application.UseCases.Products.Create;
using EasyPoint.Application.UseCases.Products.GetAll;
using EasyPoint.Application.UseCases.Stores.Create;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPoint.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Default Mediator CQRS
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        //Default Validator with fluent-validation
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
        services.AddScoped<IValidator<GetProductsQuery>, GetProductsQueryValidator>();
        services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        services.AddScoped<IValidator<CreateStoreCommand>, CreateStoreCommandValidator>();

        return services;
    }
}
