using Authentication.Infrastrunture.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Authentication.Infrastrunture.Extensions;
public static class MiddlewareExtensions
{
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}

