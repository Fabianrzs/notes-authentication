using Authentication.Domain.Ports;
using Authentication.Infrastrunture.Adapters.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using System.Text;

namespace Authentication.Infrastrunture.Extensions;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtSettings(this IServiceCollection svc, IConfiguration configuration)
    {
        svc.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        svc.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:key"]))
            };
        });

        svc.AddScoped<IJwtServices, JwtService>();


        return svc;
    }
}