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
using Microsoft.EntityFrameworkCore;
using JwtSettings = BankComplaintManagement.Infrastructure.Settings.JwtSettings;

var builder = WebApplication.CreateBuilder(args);


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







// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();





// ================================
// Database Seed
// ================================


using (var scope = app.Services.CreateScope())
{

    var seeder =
        scope.ServiceProvider
        .GetRequiredService<AdminSeeder>();


    await seeder.SeedAsync();

}


app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();






app.Run();

