using System.Net;
using System.Net.Http.Json;
using BankComplaintManagement.Application.DTOs.Auth;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankComplaintManagement.IntegrationTests;

public class AuthIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthIntegrationTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_Should_Return_Token_When_Credentials_Are_Valid()
    {
        var request = new LoginRequest
        {
            Email = CustomWebApplicationFactory.TestClientEmail,
            Password = CustomWebApplicationFactory.TestClientPassword
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/auth/login",
                request);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        var options = new JsonSerializerOptions
        {
    	    PropertyNameCaseInsensitive = true
        };

        options.Converters.Add(
       	    new JsonStringEnumConverter());

        var result =
            await response.Content
                .ReadFromJsonAsync<LoginResponse>(options);

        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.Token));
        Assert.False(string.IsNullOrWhiteSpace(result.RefreshToken));
        Assert.Equal(
            CustomWebApplicationFactory.TestClientEmail,
            result.Email);
    }
}