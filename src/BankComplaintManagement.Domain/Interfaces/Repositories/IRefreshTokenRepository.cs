using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {

        Task<RefreshToken?> GetByTokenAsync(
            string token);


        Task AddAsync(
            RefreshToken refreshToken);


        void Update(
            RefreshToken refreshToken);

    }
}
