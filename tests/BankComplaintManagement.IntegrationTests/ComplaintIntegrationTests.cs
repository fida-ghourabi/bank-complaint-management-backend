using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BankComplaintManagement.Application.DTOs.Auth;
using BankComplaintManagement.Application.DTOs.Complaints;

namespace BankComplaintManagement.IntegrationTests;

public class ComplaintIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public ComplaintIntegrationTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateComplaint_Should_Return_Created_When_Request_Is_Valid()
    {
        // 1. Login
        var loginRequest = new LoginRequest
        {
            Email = CustomWebApplicationFactory.TestClientEmail,
            Password = CustomWebApplicationFactory.TestClientPassword
        };

        var loginResponse =
            await _client.PostAsJsonAsync(
                "/api/auth/login",
                loginRequest);

        Assert.Equal(
            HttpStatusCode.OK,
            loginResponse.StatusCode);

        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        jsonOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());

        var loginResult =
            await loginResponse.Content
                .ReadFromJsonAsync<LoginResponse>(jsonOptions);

        Assert.NotNull(loginResult);
        Assert.False(
            string.IsNullOrWhiteSpace(loginResult.Token));

        // 2. Add JWT token
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                loginResult.Token);

        // 3. Create complaint request
        using var form = new MultipartFormDataContent();

        form.Add(
            new StringContent("carte"),
            "Category");

        form.Add(
            new StringContent("Carte bloquée"),
            "SubCategory");

        form.Add(
            new StringContent(
                _factory.TestBankAccountId.ToString()),
            "RelatedBankAccountId");

        form.Add(
            new StringContent("Ma carte est bloquée"),
            "Subject");

        form.Add(
            new StringContent(
                "Ma carte bancaire ne fonctionne plus."),
            "Description");

        form.Add(
            new StringContent(
                DateTime.UtcNow.ToString("O")),
            "IncidentDate");

        form.Add(
            new StringContent("10:30:00"),
            "IncidentTime");

        form.Add(
            new StringContent("Tunis"),
            "Location");

        form.Add(
            new StringContent("web"),
            "Channel");

        form.Add(
            new StringContent("normale"),
            "Priority");

        // 4. Send request
        var response =
            await _client.PostAsync(
                "/api/complaints",
                form);

        // 5. Verify HTTP 201 Created
        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);
    }
}