using IUstaFinalProject.Application.Abstraction.Services;
using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities.Identity;
using IUstaFinalProject.Persistence.Contexts;
using IUstaFinalProject.Persistence.Repositories;
using IUstaFinalProject.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            services.AddScoped<IAgreementReadRepository, AgreementReadRepository>();
            services.AddScoped<IAgreementWriteRepository, AgreementWriteRepository>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<IMessageReadRepository, MessageReadRepository>();
            services.AddScoped<IMessageWriteRepository, MessageWriteRepository>();

            services.AddScoped<INotificationReadRepository, NotificationReadRepository>();
            services.AddScoped<INotificationWriteRepository, NotificationWriteRepository>();

            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();

            services.AddScoped<IReviewReadRepository, ReviewReadRepository>();
            services.AddScoped<IReviewWriteRepository, ReviewWriteRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
