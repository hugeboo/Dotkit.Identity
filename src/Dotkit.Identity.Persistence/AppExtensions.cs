using Dotkit.Identity.Persistence.Repository;
using Dotkit.Identity.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Persistence
{
    public static class AppExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, 
            IConfiguration configuration, string connectionStringName)
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);

            services.AddDbContext<IdentityDbContext>(
               options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
