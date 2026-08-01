using BankComplaintManagement.Application.DTOs.Attachments;
using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Mappings;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class AttachmentService
       : IAttachmentService
    {


        private readonly IAttachmentRepository _repository;

        private readonly IFileStorageService _storage;



        public AttachmentService(
            IAttachmentRepository repository, IFileStorageService storage)
        {
            _repository = repository;
            _storage = storage;
        }















        public async Task<FileDownloadDto> DownloadAsync(Guid id)
        {
            var attachment =
                await _repository.GetByIdAsync(id);

            if (attachment == null)
                throw new NotFoundException("Fichier introuvable.");

            var bytes =
                await _storage.DownloadFileAsync(
                    attachment.FilePath);

            return new FileDownloadDto
            {
                FileName = attachment.FileName,
                ContentType = attachment.ContentType,
                Content = bytes
            };
        }

    }

}
