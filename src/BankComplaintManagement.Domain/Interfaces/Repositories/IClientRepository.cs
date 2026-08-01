using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(Guid id);
        Task<Client?> GetByCustomerNumberAsync(string customerNumber);
        Task<Client?> GetByCinAsync(string cin);
        Task<Client?> GetByEmailAsync(string email);

        Task<IReadOnlyList<Client>> GetByStatusAsync(
            ClientStatus status);
        Task<IReadOnlyList<Client>> SearchAsync(
            string keyword);
        Task<int> CountAsync();
        Task<int> CountByStatusAsync(
            ClientStatus status);

        Task<int> CountTotalClientsAsync();


        Task<int> CountActiveClientsAsync();


        Task<int> CountBlockedClientsAsync();


        Task<int> CountByClientAsync(
            Guid clientId);


        Task<int> CountOpenByClientAsync(
            Guid clientId);
        Task<bool> ExistsByCinAsync(string cin);
        Task<bool> ExistsByEmailAsync(string email);

         Task<IReadOnlyList<Client>> GetPagedAsync(
              int pageNumber,
              int pageSize);

        Task<IReadOnlyList<Client>> GetAllAsync();
        Task AddAsync(Client client);

        void Update(Client client);

        Task<Client?> GetByIdWithAccountsAsync(Guid id);
    }
}
