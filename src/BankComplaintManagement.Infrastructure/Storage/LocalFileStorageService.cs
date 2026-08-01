using BankComplaintManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Storage
{
    public class FileStorageService : IFileStorageService
    {

        private readonly string _uploadFolder;


        public FileStorageService()
        {
            _uploadFolder =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "uploads");


            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }

        }



        public async Task<string> SaveFileAsync(
            IFormFile file)
        {

            var fileName =
                $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";


            var fullPath =
                Path.Combine(
                    _uploadFolder,
                    fileName);



            using var stream =
                new FileStream(
                    fullPath,
                    FileMode.Create);



            await file.CopyToAsync(stream);



            return fileName;

        }





        public async Task<byte[]> DownloadFileAsync(
            string path)
        {

            var fullPath =
                Path.Combine(
                    _uploadFolder,
                    path);



            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(
                    "Fichier introuvable.");
            }



            return await File.ReadAllBytesAsync(fullPath);

        }





        public Task DeleteFileAsync(
            string path)
        {

            var fullPath =
                Path.Combine(
                    _uploadFolder,
                    path);



            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }


            return Task.CompletedTask;

        }

    }
}