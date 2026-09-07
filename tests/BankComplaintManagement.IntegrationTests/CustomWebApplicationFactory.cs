using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Infrastructure.Persistence;
using BankComplaintManagement.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.MsSql;

namespace BankComplaintManagement.IntegrationTests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer =
        new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public Guid TestClientId { get; private set; }

    public Guid TestBankAccountId { get; private set; }

    public const string TestClientEmail =
        "integration.client@test.com";

    public const string TestClientPassword =
        "Password123!";

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
    }

    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration(
            (context, config) =>
            {
                var testSettings =
                    new Dictionary<string, string?>
                    {
                        ["ConnectionStrings:DefaultConnection"] =
                            _sqlContainer.GetConnectionString(),

                        ["AdminSettings:FirstName"] =
                            "Test",

                        ["AdminSettings:LastName"] =
                            "Admin",

                        ["AdminSettings:Email"] =
                            "admin@test.com",

                        ["AdminSettings:Password"] =
                            "AdminPassword123!",

                        ["JwtSettings:SecretKey"] =
                            "integration-test-secret-key-12345678901234567890",

                        ["JwtSettings:Issuer"] =
                            "bank-complaint-integration-tests",

                        ["JwtSettings:Audience"] =
                            "bank-complaint-integration-tests",

                        ["JwtSettings:ExpirationMinutes"] =
                            "60"
                    };

                config.AddInMemoryCollection(testSettings);
            });
    }

    protected override IHost CreateHost(
        IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope =
            host.Services.CreateScope();

        var db =
            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

        SeedTestDataAsync(
            scope.ServiceProvider,
            db)
            .GetAwaiter()
            .GetResult();

        return host;
    }

    private async Task SeedTestDataAsync(
        IServiceProvider services,
        ApplicationDbContext db)
    {
        var existingClient =
            await db.Clients
                .FirstOrDefaultAsync(
                    c => c.Email == TestClientEmail);

        if (existingClient != null)
        {
            TestClientId = existingClient.Id;

            var existingAccount =
                await db.BankAccounts
                    .FirstOrDefaultAsync(
                        a => a.ClientId == TestClientId);

            if (existingAccount != null)
            {
                TestBankAccountId =
                    existingAccount.Id;
            }

            return;
        }

        var passwordService =
            services.GetRequiredService<IPasswordService>();

        var client =
            new Client(
                "Integration",
                "Client",
                TestClientEmail,
                passwordService.HashPassword(
                    TestClientPassword),
                "12345678",
                "20123456");

        await db.Clients.AddAsync(client);

        await db.SaveChangesAsync();

        var account =
            new BankAccount(
                "INT-ACCOUNT-001",
                "TN590000000000000000000001",
                AccountType.Courant,
                1000m,
                client.Id);

        await db.BankAccounts.AddAsync(account);

        await db.SaveChangesAsync();

        TestClientId = client.Id;
        TestBankAccountId = account.Id;
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
    await _sqlContainer.DisposeAsync();
    }
}