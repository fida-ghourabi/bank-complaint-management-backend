using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using BankComplaintManagement.Infrastructure.Persistence;
using BankComplaintManagement.Infrastructure.Persistence.Seed;
using BankComplaintManagement.Infrastructure.Repositories;
using BankComplaintManagement.Infrastructure.Secutity;
using BankComplaintManagement.Infrastructure.Services;
using BankComplaintManagement.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {



            // ===================================
            // Database
            // ===================================


            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseSqlServer(
                    configuration
                    .GetConnectionString(
                        "DefaultConnection"
                    ))
            );


            services.Configure<AdminSettings>(
                configuration.GetSection("AdminSettings"));


            // ===================================
            // Repositories
            // ===================================


            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IAgentRepository, AgentRepository>();

            services.AddScoped<IComplaintRepository, ComplaintRepository>();

            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<IAttachmentRepository, AttachmentRepository>();

            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            services.AddScoped<IBankCardRepository, BankCardRepository>();

            services.AddScoped<IUserRepository, UserRepository>();


            // ===================================
            // Unit Of Work
            // ===================================


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infrastructure services


            services.AddScoped<IPasswordService, PasswordService>();

            services.AddScoped<IJwtService, JwtService>();

            return services;

        }


    }
}
