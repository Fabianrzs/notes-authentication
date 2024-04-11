using Authentication.Infrastrunture.Adapters;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authentication.Infrastrunture.Extensions;

public static class ValidationExtension
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        Assembly validationAssembly = Assembly.Load(ApiConstants.ApplicationProject);
        services.AddValidatorsFromAssembly(validationAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
