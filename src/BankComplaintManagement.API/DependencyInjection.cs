using BankComplaintManagement.API.Filters;
using BankComplaintManagement.Application.Validators.Auth;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BankComplaintManagement.API
{
    public static  class DependencyInjection
    {
         public static IServiceCollection AddPresentation(
            this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });

            services.AddScoped<ValidationFilter>();

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            return services;
        }
    }
}
