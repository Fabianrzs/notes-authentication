using Common.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Authentication.Infrastrunture.Context;
using Authentication.Infrastrunture.Extensions;
using Authentication.Infrastrunture.Inicialize;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastrunture;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediator();
        services.AddDomainServices();
        services.AddPesistence(config);
        services.AddValidator();
        services.AddMapper();
        services.AddJwtSettings(config);
        services.AddSwagger();
        services.AddSerializer();
        services.AddServiceBusIntegrationPublisher(config);
    }

    public static void UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwaggers(env);
        app.UseExceptionMiddleware();
        app.UseAuthentication();
        app.UseAuthorization();
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        InitializeDatabase(scope);
        app.UseHttpsRedirection();
    }

    private static void InitializeDatabase(IServiceScope scope)
    {
        if (scope == null) return;
        MigrateDbContextExtensions.MigrateDbContextServices<AppDbContext>(scope);
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (!context.Database.CanConnect()) return;
        var start = new Start(context!);
        start.Seed().Wait();
    }
}
