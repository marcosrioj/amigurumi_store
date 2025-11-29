using AmigurumiStore.Identity.Application.Data;
using AmigurumiStore.Identity.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AmigurumiStore.Identity.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services, string connectionString, JwtOptions jwtOptions)
    {
        services.Configure<JwtOptions>(_ => jwtOptions);
        services.AddSingleton<TokenService>();
        services.AddScoped<IPasswordHasher<Models.User>, PasswordHasher<Models.User>>();

        services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });

        services.AddAuthorization();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ServiceCollectionExtensions>());
        return services;
    }
}
