using IUstaFinalProject.Application.Abstraction.Services;
using IUstaFinalProject.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection collection)
        {
            collection.AddScoped<IMailService,MailService>();
            collection.AddScoped<ILoginRegisterService,LoginRegisterService>();
        }
    }
}
