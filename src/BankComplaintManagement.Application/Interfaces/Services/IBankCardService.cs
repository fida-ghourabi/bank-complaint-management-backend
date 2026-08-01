using BankComplaintManagement.Application.DTOs.BankCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IBankCardService
    {


        Task<IReadOnlyList<BankCardInfoDto>>
            GetByClientAsync(
                Guid clientId);


    }
}
