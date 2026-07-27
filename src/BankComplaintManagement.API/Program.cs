using BankComplaintManagement.Application;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Services;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using BankComplaintManagement.Infrastructure;
using BankComplaintManagement.Infrastructure.Persistence;
using BankComplaintManagement.Infrastructure.Persistence.Seed;
using BankComplaintManagement.Infrastructure.Repositories;
using BankComplaintManagement.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using JwtSettings = BankComplaintManagement.Infrastructure.Settings.JwtSettings;

var builder = WebApplication.CreateBuilder(args);

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


// Controllers

//builder.Services.AddControllers();






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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
