using BankComplaintManagement.Application.DTOs.Auth;
using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Mappings;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class AuthService : IAuthService
    {


        private readonly IUserRepository _userRepository;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IPasswordService _passwordService;

        private readonly IJwtService _jwtService;

        private readonly IRefreshTokenService _refreshTokenService;

        private readonly IUnitOfWork _unitOfWork;



        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordService passwordService,
            IJwtService jwtService,
            IRefreshTokenService refreshTokenService,
            IUnitOfWork unitOfWork)
        {

            _userRepository = userRepository;

            _refreshTokenRepository = refreshTokenRepository;

            _passwordService = passwordService;

            _jwtService = jwtService;

            _refreshTokenService = refreshTokenService;

            _unitOfWork = unitOfWork;

        }






        public async Task<LoginResponse> LoginAsync(
            LoginRequest request)
        {


            var user =
                await _userRepository
                .GetByEmailAsync(request.Email);



            if (user == null)
            {
                throw new UnauthorizedException(
                    "Email ou mot de passe incorrect");
            }





            bool validPassword =
                _passwordService
                .VerifyPassword(
                    request.Password,
                    user.PasswordHash);



            if (!validPassword)
            {
                throw new UnauthorizedException(
                    "Email ou mot de passe incorrect");
            }






            var accessToken =
                _jwtService
                .GenerateToken(user);





            var refreshTokenValue =
                _refreshTokenService
                .Generate();





            var refreshToken =
                new RefreshToken(
                    refreshTokenValue,
                    DateTime.UtcNow.AddDays(7),
                    user.Id);





            await _refreshTokenRepository
                .AddAsync(refreshToken);


            await _unitOfWork
               .SaveChangesAsync();


            return user.ToLoginResponse(
                accessToken,
                refreshTokenValue,
                DateTime.UtcNow.AddMinutes(15));

        }









        public async Task<LoginResponse> RefreshTokenAsync(
            String refreshToken)
        {


            var existingToken =
                await _refreshTokenRepository
                .GetByTokenAsync(refreshToken);




            if (existingToken == null)
            {
                throw new UnauthorizedException(
                    "Refresh token invalide");
            }





            if (existingToken.IsRevoked)
            {
                throw new UnauthorizedException(
                    "Refresh token révoqué");
            }





            if (existingToken.IsExpired())
            {
                throw new UnauthorizedException(
                    "Refresh token expiré");
            }






            var user =
                await _userRepository
                .GetByIdAsync(
                    existingToken.UserId);





            if (user == null)
            {
                throw new NotFoundException(
                    "Utilisateur introuvable");
            }






            // Révoquer ancien refresh token

            existingToken.Revoke();


            _refreshTokenRepository
                .Update(existingToken);







            // Générer nouveau JWT

            var newAccessToken =
                _jwtService
                .GenerateToken(user);






            // Générer nouveau RefreshToken

            var newRefreshTokenValue =
                _refreshTokenService
                .Generate();






            var newRefreshToken =
                new RefreshToken(
                    newRefreshTokenValue,
                    DateTime.UtcNow.AddDays(7),
                    user.Id);






            await _refreshTokenRepository
                .AddAsync(newRefreshToken);


            await _unitOfWork
               .SaveChangesAsync();



            return user.ToLoginResponse(
            newAccessToken,
            newRefreshTokenValue,
            DateTime.UtcNow.AddMinutes(15));

        }









        public async Task LogoutAsync(
            String refreshToken)
        {


            var token =
                await _refreshTokenRepository
                .GetByTokenAsync(refreshToken);



            if (token == null)
            {
                throw new NotFoundException(
                    "Token introuvable");
            }




            token.Revoke();



            _refreshTokenRepository
                .Update(token);

            await _unitOfWork
               .SaveChangesAsync();

        }

    }
}
