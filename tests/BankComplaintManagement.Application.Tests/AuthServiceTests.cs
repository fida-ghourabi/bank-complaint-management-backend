using BankComplaintManagement.Application.DTOs.Auth;
using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Services;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using Moq;

namespace BankComplaintManagement.Application.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task Should_Login_Successfully_When_Credentials_Are_Valid()
    {
        // Arrange
        var user = new Agent(
            "Fida",
            "Ghourabi",
            "fida@example.com",
            "hashed-password",
            "AG001",
            "Agent",
            "Service Client",
            "12345678"
        );

        var request = new LoginRequest
        {
            Email = "fida@example.com",
            Password = "Password123"
        };

        var userRepository = new Mock<IUserRepository>();
        var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
        var passwordService = new Mock<IPasswordService>();
        var jwtService = new Mock<IJwtService>();
        var refreshTokenService = new Mock<IRefreshTokenService>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userRepository
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        passwordService
            .Setup(x => x.VerifyPassword(
                request.Password,
                user.PasswordHash))
            .Returns(true);

        jwtService
            .Setup(x => x.GenerateToken(user))
            .Returns("access-token");

        refreshTokenService
            .Setup(x => x.Generate())
            .Returns("refresh-token");

        var service = new AuthService(
            userRepository.Object,
            refreshTokenRepository.Object,
            passwordService.Object,
            jwtService.Object,
            refreshTokenService.Object,
            unitOfWork.Object
        );

        // Act
        var result = await service.LoginAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("access-token", result.Token);
        Assert.Equal("refresh-token", result.RefreshToken);
        Assert.Equal(user.Id, result.UserId);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal("Fida Ghourabi", result.FullName);
        Assert.Equal(user.Role, result.Role);

        refreshTokenRepository.Verify(
            x => x.AddAsync(It.IsAny<RefreshToken>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Throw_UnauthorizedException_When_User_Does_Not_Exist()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "unknown@example.com",
            Password = "Password123"
        };

        var userRepository = new Mock<IUserRepository>();
        var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
        var passwordService = new Mock<IPasswordService>();
        var jwtService = new Mock<IJwtService>();
        var refreshTokenService = new Mock<IRefreshTokenService>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userRepository
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync((User?)null);

        var service = new AuthService(
            userRepository.Object,
            refreshTokenRepository.Object,
            passwordService.Object,
            jwtService.Object,
            refreshTokenService.Object,
            unitOfWork.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedException>(
            () => service.LoginAsync(request));
    }
}