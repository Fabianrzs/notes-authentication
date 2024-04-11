using Authentication.Domain.Ports;
using Authentication.Infrastructure.Adapters;
using Authentication.Infrastrunture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastrunture.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPesistence(this IServiceCollection svc, IConfiguration config)
    {
        svc.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        svc.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return svc;
    }
}
