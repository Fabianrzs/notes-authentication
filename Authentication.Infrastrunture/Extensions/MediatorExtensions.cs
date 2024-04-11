using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authentication.Infrastrunture.Extensions;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.Load(ApiConstants.ApplicationProject), Assembly.GetExecutingAssembly());
        return services;
    }
}
