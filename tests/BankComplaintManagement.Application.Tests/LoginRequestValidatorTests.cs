using BankComplaintManagement.Application.DTOs.Auth;
using BankComplaintManagement.Application.Validators.Auth;

namespace BankComplaintManagement.Application.Tests;

public class LoginRequestValidatorTests
{
    [Fact]
    public void Should_Pass_When_Login_Data_Is_Valid()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "test@example.com",
            Password = "Password123"
        };

        var validator = new LoginRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_Login_Data_Is_Invalid()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "email-invalide",
            Password = ""
        };

        var validator = new LoginRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
}