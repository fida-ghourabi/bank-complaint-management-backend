using BankComplaintManagement.Application.DTOs.Messages;
using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Mappings;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class MessageService : IMessageService
    {

        private readonly IComplaintRepository _complaintRepository;


        private readonly IMessageRepository _messageRepository;


        private readonly IAttachmentRepository _attachmentRepository;

        private readonly INotificationService _notificationService;

        private readonly IFileStorageService _storage;

        private readonly IUnitOfWork _unitOfWork;



        public MessageService(
            IComplaintRepository complaintRepository,
            IMessageRepository messageRepository,
            IAttachmentRepository attachmentRepository,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IFileStorageService storage )
        {

            _complaintRepository = complaintRepository;

            _messageRepository = messageRepository;

            _attachmentRepository = attachmentRepository;

            _notificationService = notificationService;

            _unitOfWork = unitOfWork;
            _storage = storage;
        }






        public async Task<MessageDto> CreateAsync(
     Guid complaintId,
     CreateMessageRequest request, bool isAgent)
        {

            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new NotFoundException(
                    "Réclamation introuvable.");
            }




            var message =
                new Message(
                    request.Text,
                    isAgent,
                    complaintId);




            complaint.AddMessage(message);




            await _messageRepository
                .AddAsync(message);





            if (request.Attachments.Any())
            {

                foreach (var file in request.Attachments)
                {
                    var path = await _storage.SaveFileAsync(file);

                    var attachment = new Attachment(
                        file.FileName,
                        path,
                        file.ContentType,
                        file.Length,
                        messageId: message.Id);

                    await _attachmentRepository.AddAsync(attachment);
                }

            }

                // Si le message vient du client

                if (isAgent == false)
                {

                    await _notificationService
                        .NotifyCustomerReplyAsync(
                            complaintId);

                }


                // Si le message vient de l'agent
                else 
                {

                    await _notificationService
                        .NotifyAgentReplyAsync(
                            complaintId);

                }

                await _unitOfWork
                    .SaveChangesAsync();

            




            return message.ToDto();

        }

    }
}
