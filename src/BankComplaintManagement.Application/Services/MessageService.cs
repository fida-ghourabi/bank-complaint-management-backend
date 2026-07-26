using BankComplaintManagement.Application.DTOs.Messages;
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



        private readonly IUnitOfWork _unitOfWork;



        public MessageService(
            IComplaintRepository complaintRepository,
            IMessageRepository messageRepository,
            IAttachmentRepository attachmentRepository,
            INotificationService notificationService,
            IUnitOfWork unitOfWork)
        {

            _complaintRepository = complaintRepository;

            _messageRepository = messageRepository;

            _attachmentRepository = attachmentRepository;

            _notificationService = notificationService;

            _unitOfWork = unitOfWork;

        }






        public async Task<MessageDto> CreateAsync(
     Guid complaintId,
     CreateMessageRequest request)
        {

            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new KeyNotFoundException(
                    "Réclamation introuvable.");
            }




            var message =
                new Message(
                    request.Text,
                    request.IsAgent,
                    complaintId);




            complaint.AddMessage(message);




            await _messageRepository
                .AddAsync(message);





            if (request.Attachments.Any())
            {

                foreach (var attachmentRequest in request.Attachments)
                {

                    var attachment =
                        new Attachment(
                            attachmentRequest.FileName,
                            attachmentRequest.FilePath,
                            attachmentRequest.ContentType,
                            attachmentRequest.FileSize,
                            message.Id);



                    await _attachmentRepository
                        .AddAsync(attachment);

                }



                // Si le message vient du client

                if (request.IsAgent == false)
                {

                    await _notificationService
                        .NotifyCustomerReplyAsync(
                            complaintId);

                }


                // Si le message vient de l'agent
                if (request.IsAgent == true)
                {

                    await _notificationService
                        .NotifyAgentReplyAsync(
                            complaintId);

                }

                await _unitOfWork
                    .SaveChangesAsync();

            }




            return message.ToDto();

        }

    }
}
