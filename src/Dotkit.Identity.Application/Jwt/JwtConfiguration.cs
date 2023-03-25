using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Application.Jwt
{
    internal sealed class JwtConfiguration
    {
        public SymmetricSecurityKey Secret { get; }
        public int LifetimeInMinutes { get; }

        public JwtConfiguration(string secretBase64, int lifetime)
        {
            Secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretBase64));
            LifetimeInMinutes = lifetime;
        }
    }

    internal static class JwtConfigurationExtension
    {
        public static JwtConfiguration GetJwtConfiguration(this IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection("JWT");

            string? secret = section["Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("Параметр конфигурации JWT:Secret не найден");
            }

            string? lifetimeInMinutesStr = section["LifetimeInMinutes"];
            if (string.IsNullOrEmpty(lifetimeInMinutesStr))
            {
                throw new ArgumentException("Параметр конфигурации JWT:LifetimeInMinutes не найден");
            }

            if (!int.TryParse(lifetimeInMinutesStr, out int lifetime) || lifetime <= 0)
            {
                throw new ArgumentException($"Параметр конфигурации JWT:LifetimeInMinutes не валиден: {lifetimeInMinutesStr}");
            }

            return new JwtConfiguration(secret, lifetime);
        }
    }
}
