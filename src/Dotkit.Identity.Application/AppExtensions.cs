using Dotki.Identity.Application.Jwt;
using Dotkit.Identity.Application.Auth;
using Dotkit.Identity.Application.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotkit.Identity.Application
{
    public static class AppExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITokenService, TokenService>(_ => new TokenService(configuration.GetJwtConfiguration()));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            return services;
        }
    }
}