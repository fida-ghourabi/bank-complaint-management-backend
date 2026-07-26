using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {


            // ============================
            // Services Application
            // ============================


            services.AddScoped<IComplaintService, ComplaintService>();

            services.AddScoped<IClientService, ClientService>();

            services.AddScoped<IAgentService, AgentService>();

            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IBankAccountService, BankAccountService>();



            return services;

        }

    }
}
