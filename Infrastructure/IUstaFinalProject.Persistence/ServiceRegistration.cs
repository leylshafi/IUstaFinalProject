using IUstaFinalProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Persistence
{
    public static class ServiceRegistration
    {
        private static readonly IConfigurationBuilder _configurationBuilder;
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.ConnectionString,
                op => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)), ServiceLifetime.Transient);

        }
    }
}
