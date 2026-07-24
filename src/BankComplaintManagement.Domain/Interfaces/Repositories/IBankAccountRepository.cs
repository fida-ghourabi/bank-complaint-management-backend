using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IBankAccountRepository
    {
        // Récupérer un compte par Id
        Task<BankAccount?> GetByIdAsync(
            Guid id);



        // Récupérer les comptes d'un client
        Task<IReadOnlyList<BankAccount>> GetByClientIdAsync(
            Guid clientId);



        // Récupérer un compte par numéro
        Task<BankAccount?> GetByAccountNumberAsync(
            string accountNumber);



        // Récupérer un compte par IBAN
        Task<BankAccount?> GetByIbanAsync(
            string iban);



        // Vérifier si un numéro de compte existe
        Task<bool> ExistsByAccountNumberAsync(
            string accountNumber);



        // Vérifier si un IBAN existe
        Task<bool> ExistsByIbanAsync(
            string iban);



        // Ajouter un compte
        Task AddAsync(
            BankAccount bankAccount);



        // Modifier un compte
        void Update(
            BankAccount bankAccount);



        // Supprimer un compte
        void Delete(
            BankAccount bankAccount);



        // Compter les comptes d'un client
        Task<int> CountByClientAsync(
            Guid clientId);
    }
}
