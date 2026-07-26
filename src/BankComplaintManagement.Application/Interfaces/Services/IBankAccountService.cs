using BankComplaintManagement.Application.DTOs.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IBankAccountService
    {
        Task<IReadOnlyList<BankAccountInfoDto>> GetByClientAsync(
           Guid clientId);



       
    }
}
