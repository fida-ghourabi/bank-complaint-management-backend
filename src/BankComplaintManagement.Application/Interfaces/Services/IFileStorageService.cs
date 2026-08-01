using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IFileStorageService
    {


        Task<string> SaveFileAsync(IFormFile file);

        Task<byte[]> DownloadFileAsync(string path);

        Task DeleteFileAsync(string path);


    }
}
