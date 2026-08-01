using BankComplaintManagement.Application.DTOs.Clients;
using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Mappings;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;


        private readonly IComplaintRepository _complaintRepository;


        private readonly IUnitOfWork _unitOfWork;


        private readonly IPasswordService _passwordService;



        public ClientService(
            IClientRepository clientRepository,
            IComplaintRepository complaintRepository,
            IUnitOfWork unitOfWork,
            IPasswordService passwordService)
        {

            _clientRepository = clientRepository;

            _complaintRepository = complaintRepository;

            _unitOfWork = unitOfWork;

            _passwordService = passwordService;

        }


        public async Task<ClientDto> RegisterAsync(
            RegisterClientRequest request)
        {


            // ============================
            // Validation
            // ============================


            if (request.Password != request.ConfirmPassword)
            {
                throw new BadRequestException(
                    "Les mots de passe ne correspondent pas.");
            }




            bool emailExists =
                await _clientRepository
                .ExistsByEmailAsync(request.Email);



            if (emailExists)
            {
                throw new ConflictException(
                    "Cet email existe déjà.");
            }




            bool cinExists =
                await _clientRepository
                .ExistsByCinAsync(request.CIN);



            if (cinExists)
            {
                throw new ConflictException(
                    "Ce CIN existe déjà.");
            }





            // ============================
            // Hash Password
            // ============================


            var passwordHash =
                _passwordService
                .HashPassword(request.Password);





            // ============================
            // Création Client
            // ============================


            var client =
                new Client(

                    request.FirstName,

                    request.LastName,

                    request.Email,

                    passwordHash,

                    request.CIN,

                    request.PhoneNumber

                );





            await _clientRepository
                .AddAsync(client);





            await _unitOfWork
                .SaveChangesAsync();






            return client.ToDto();

        }


        // ===================================
        // Profil client
        // ===================================


        public async Task<ClientProfileDto> GetProfileAsync(
            Guid clientId)
        {

            var client =
                await _clientRepository
                .GetByIdWithAccountsAsync(clientId);



            if (client == null)
                throw new NotFoundException(
                "Client introuvable.");



            return client.ToProfileDto();

        }






        public async Task UpdateProfileAsync(
            Guid clientId,
            UpdateClientProfileRequest request)
        {

            var client =
                await _clientRepository
                .GetByIdAsync(clientId);



            if (client == null)
            {
                throw new NotFoundException(
                    "Client introuvable.");
            }




            client.FirstName =
                request.FirstName;


            client.LastName =
                request.LastName;


            client.Email =
                request.Email;


            client.PhoneNumber =
                request.PhoneNumber;



            _clientRepository.Update(client);



            await _unitOfWork.SaveChangesAsync();

        }








        public async Task ChangePasswordAsync(
            Guid clientId,
            ChangePasswordRequest request)
        {


            var client =
                await _clientRepository
                .GetByIdAsync(clientId);



            if (client == null)
            {
                throw new NotFoundException(
                    "Client introuvable.");
            }





            if (!_passwordService.VerifyPassword(
                request.OldPassword,
                client.PasswordHash))
            {
                throw new UnauthorizedException(
                    "Ancien mot de passe incorrect.");
            }





            if (request.NewPassword !=
               request.ConfirmPassword)
            {
                throw new BadRequestException(
                    "Les mots de passe ne correspondent pas.");
            }





            client.PasswordHash =
                _passwordService.HashPassword(
                    request.NewPassword);





            _clientRepository.Update(client);


            await _unitOfWork.SaveChangesAsync();

        }








        // ===================================
        // Administration
        // ===================================


        public async Task<ClientDetailsDto> GetDetailsAsync(
            Guid clientId)
        {

            var client =
                await _clientRepository
                .GetByIdAsync(clientId);



            if (client == null)
               throw new NotFoundException(
                    "Client introuvable.");




            return client.ToDetailsDto();

        }







        public async Task<IReadOnlyList<ClientListDto>> GetAllAsync()
        {

            var clients =
                await _clientRepository
                .GetPagedAsync(1, 1000);



            return clients
                .Select(c => c.ToListDto())
                .ToList();

        }




        public async Task<IReadOnlyList<ClientListDto>> GetAllClientAsync()
        {

            var clients =
                await _clientRepository
                .GetAllAsync();



            return clients
                .Select(c => c.ToListDto())
                .ToList();

        }


        public async Task<IReadOnlyList<ClientListDto>> GetByStatusAsync(
            ClientStatus status)
        {

            var clients =
                await _clientRepository
                .GetByStatusAsync(status);



            return clients
                .Select(c => c.ToListDto())
                .ToList();

        }








        public async Task UpdateStatusAsync(
            Guid clientId,
            UpdateClientStatusRequest request)
        {

            var client =
                await _clientRepository
                .GetByIdAsync(clientId);



            if (client == null)
            {
                throw new NotFoundException(
                    "Client introuvable.");
            }





            client.Status =
                request.Status;



            _clientRepository.Update(client);



            await _unitOfWork.SaveChangesAsync();

        }









        // ===================================
        // Statistiques
        // ===================================


        public async Task<int> CountTotalClientsAsync()
        {
            return await _clientRepository
                .CountTotalClientsAsync();
        }





        public async Task<int> CountActiveClientsAsync()
        {
            return await _clientRepository
                .CountActiveClientsAsync();
        }





        public async Task<int> CountBlockedClientsAsync()
        {
            return await _clientRepository
                .CountBlockedClientsAsync();
        }







       

    }
}


