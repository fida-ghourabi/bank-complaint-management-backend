using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IBankCardRepository
    {

        // Récupérer une carte par Id
        Task<BankCard?> GetByIdAsync(
            Guid id);



        // Récupérer toutes les cartes d'un compte
        Task<IReadOnlyList<BankCard>> GetByBankAccountIdAsync(
            Guid bankAccountId);



        // Récupérer une carte par numéro
        Task<BankCard?> GetByCardNumberAsync(
            string cardNumber);



        // Vérifier existence numéro carte
        Task<bool> ExistsByCardNumberAsync(
            string cardNumber);



        // Ajouter une carte
        Task AddAsync(
            BankCard card);



        // Modifier une carte
        void Update(
            BankCard card);



        // Supprimer une carte
        void Delete(
            BankCard card);



        // Compter les cartes d'un compte
        Task<int> CountByBankAccountAsync(
            Guid bankAccountId);

        Task<IReadOnlyList<BankCard>> GetByClientIdAsync(
             Guid clientId);
    }
}
