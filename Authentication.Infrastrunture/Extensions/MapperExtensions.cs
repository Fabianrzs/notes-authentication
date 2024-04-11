using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authentication.Infrastrunture.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.Load(ApiConstants.ApplicationProject));
        return services;
    }
}
