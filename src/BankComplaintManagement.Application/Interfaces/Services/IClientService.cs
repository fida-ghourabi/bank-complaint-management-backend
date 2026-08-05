using BankComplaintManagement.Application.DTOs.Clients;
using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IClientService
    {

        Task<ClientDto> RegisterAsync(
             RegisterClientRequest request);


        // ===================================
        // Profil client connecté
        // ===================================


        Task<ClientProfileDto> GetProfileAsync(
            Guid clientId);



        Task UpdateProfileAsync(
            Guid clientId,
            UpdateClientProfileRequest request);



        Task ChangePasswordAsync(
            Guid clientId,
            ClientChangePasswordRequest request);




        // ===================================
        // Détails client administration
        // ===================================


        Task<ClientDetailsDto> GetDetailsAsync(
            Guid clientId);




        // ===================================
        // Liste clients administration
        // ===================================


        Task<IReadOnlyList<ClientListDto>> GetAllAsync();


        Task<IReadOnlyList<ClientListDto>> GetAllClientAsync();


        Task<IReadOnlyList<ClientListDto>> GetByStatusAsync(
            ClientStatus status);




        // ===================================
        // Activation / blocage
        // ===================================


        Task UpdateStatusAsync(
            Guid clientId,
            UpdateClientStatusRequest request);




        // ===================================
        // Statistiques clients
        // ===================================


        Task<int> CountTotalClientsAsync();



        Task<int> CountActiveClientsAsync();



        Task<int> CountBlockedClientsAsync();






    }
}
