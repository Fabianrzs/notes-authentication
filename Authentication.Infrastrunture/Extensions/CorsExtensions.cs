using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace Authentication.Infrastrunture.Extensions;

public static class CorsExtensions
{
    private static string corsPolicy = "CorsPolicy";
    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
         {
             options.AddPolicy(name: corsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
         });
        return services;
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(corsPolicy);
        return app;
    }
}
