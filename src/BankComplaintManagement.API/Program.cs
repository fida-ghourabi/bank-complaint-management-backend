using BankComplaintManagement.API;
using BankComplaintManagement.API.Filters;
using BankComplaintManagement.API.Middleware;
using BankComplaintManagement.Application;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Services;
using BankComplaintManagement.Application.Validators.Auth;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using BankComplaintManagement.Infrastructure;
using BankComplaintManagement.Infrastructure.Persistence;
using BankComplaintManagement.Infrastructure.Persistence.Seed;
using BankComplaintManagement.Infrastructure.Repositories;
using BankComplaintManagement.Infrastructure.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;
using JwtSettings = BankComplaintManagement.Infrastructure.Settings.JwtSettings;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter("OpenTelemetry", LogLevel.Debug);
builder.Logging.AddFilter("OpenTelemetry.Exporter", LogLevel.Debug);

builder.Services.AddHealthChecks();

// ================================
// OpenTelemetry
// ================================

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService("bank-complaint-api"))
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(
                    "http://tempo.monitoring.svc.cluster.local:4318"
                );
                options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
            });
    });

builder.Services.AddHttpContextAccessor();
 
// ================================
// PRESENTATION Layer
// ================================

builder.Services.AddPresentation();


builder.Services
    .Configure<JwtSettings>(
        builder.Configuration
        .GetSection("JwtSettings"));

// ================================
// Application Layer
// ================================

builder.Services.AddApplication();



// ================================
// Infrastructure Layer
// ================================

builder.Services.AddInfrastructure(
    builder.Configuration);


// CI/CD test - Argo CD automatic deployment



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Swagger
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings =
            builder.Configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>();


        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings!.Issuer,

                ValidAudience = jwtSettings.Audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtSettings.SecretKey))
            };


        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(
                    "JWT ERROR : " +
                    context.Exception.Message);

                return Task.CompletedTask;
            },


            OnChallenge = context =>
            {
                Console.WriteLine(
                    "JWT CHALLENGE : " +
                    context.Error);

                Console.WriteLine(
                    context.ErrorDescription);

                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
        policy =>
        {
            policy
            .WithOrigins(
                "http://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});


var app = builder.Build();





// ================================
// Database Seed
// ================================


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync();

    var seeder =
        scope.ServiceProvider
        .GetRequiredService<AdminSeeder>();


    await seeder.SeedAsync();

}


app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
// Swagger


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors("AngularPolicy");

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");


app.MapPrometheusScrapingEndpoint();




app.Run();

