using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Mappings;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class ComplaintService : IComplaintService
    {

        private readonly IComplaintRepository _complaintRepository;

        private readonly IClientRepository _clientRepository;

        private readonly IBankAccountRepository _bankAccountRepository;

        private readonly IBankCardRepository _bankCardRepository;

        private readonly IAgentRepository _agentRepository;

        private readonly IAttachmentRepository _attachmentRepository;

        private readonly INotificationService _notificationService;

        private readonly IFileStorageService _storage;

        private readonly IUnitOfWork _unitOfWork;



        public ComplaintService(
            IComplaintRepository complaintRepository,
            IClientRepository clientRepository,
            IBankAccountRepository bankAccountRepository,
            IBankCardRepository bankCardRepository,
            IAgentRepository agentRepository,
            IAttachmentRepository attachmentRepository,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IFileStorageService storage)
        {

            _complaintRepository = complaintRepository;

            _clientRepository = clientRepository;

            _bankAccountRepository = bankAccountRepository;

            _bankCardRepository = bankCardRepository;

            _agentRepository = agentRepository;

            _attachmentRepository = attachmentRepository;

            _notificationService = notificationService;

            _unitOfWork = unitOfWork;
            _storage = storage;
        }




        // =====================================================
        // CREATE COMPLAINT
        // =====================================================


        public async Task<ComplaintDto> CreateAsync(
            Guid clientId,
            CreateComplaintRequest request)
        {


            

                // Vérifier client

                var client =
                    await _clientRepository.GetByIdAsync(clientId);



                if (client == null)
                {
                    throw new NotFoundException(
                        "Client introuvable.");
                }



                // Vérification règle métier Client
                client.EnsureCanCreateComplaint();

                // Vérifier compte bancaire obligatoire

                var account =
                    await _bankAccountRepository
                    .GetByIdAsync(
                        request.RelatedBankAccountId);



                if (account == null)
                {
                    throw new NotFoundException(
                        "Compte bancaire introuvable.");
                }




                // Vérifier que le compte appartient au client

                if (account.ClientId != clientId)
                {
                    throw new ForbiddenException(
                        "Ce compte bancaire n'appartient pas au client.");
                }





                // Vérifier carte si présente

                if (request.RelatedBankCardId.HasValue)
                {

                    var card =
                        await _bankCardRepository
                        .GetByIdAsync(
                            request.RelatedBankCardId.Value);



                    if (card == null)
                    {
                        throw new NotFoundException(
                            "Carte bancaire introuvable.");
                    }





                    if (card.BankAccountId
                        != request.RelatedBankAccountId)
                    {

                        throw new ForbiddenException(
                            "Cette carte n'appartient pas au compte sélectionné.");

                    }

                }





                // Création de l'entité Domain

                var complaint =
                    new Complaint(
                        clientId,
                        request.RelatedBankAccountId,
                        request.RelatedBankCardId,
                        request.Category,
                        request.SubCategory,
                        request.Subject,
                        request.Description,
                        request.IncidentDate,
                        request.IncidentTime,
                        request.Location,
                        request.Channel,
                        request.Priority
                    );



                // Ajouter informations supplémentaires

                complaint.SetFinancialImpact(
                    request.FinancialImpact);



                if (!string.IsNullOrWhiteSpace(request.BranchName))
                {
                    complaint.BranchName = request.BranchName;
                }





                await _complaintRepository
                    .AddAsync(complaint);





            // Ajouter pièces jointes

            foreach (var file in request.Attachments)
            {


                var path =
                await _storage.SaveFileAsync(file);



                var attachment =
                new Attachment(
                file.FileName,
                path,
                file.ContentType,
                file.Length,
                complaintId: complaint.Id);





                await _attachmentRepository
                .AddAsync(attachment);



            }



            // Notification aux agents + admins

            await _notificationService
                    .NotifyNewComplaintAsync(
                        complaint.Id);


                await _unitOfWork.SaveChangesAsync();





                return complaint.ToDto();

            

         

        }









        // =====================================================
        // GET BY ID
        // =====================================================


        public async Task<ComplaintDto> GetByIdAsync(
            Guid complaintId)
        {


            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new NotFoundException(
                 "Réclamation introuvable.");
            }



            return complaint.ToDto();

        }








        // =====================================================
        // GET DETAILS
        // =====================================================


        public async Task<ComplaintDetailsDto> GetDetailsAsync(Guid complaintId)
        {

            var complaint =
                await _complaintRepository
                .GetByIdWithDetailsAsync(complaintId);



            if (complaint == null)
            {
               throw new NotFoundException(
                "Réclamation introuvable.");
            }



            var account =
                await _bankAccountRepository
                .GetByIdAsync(
                    complaint.RelatedBankAccountId);



            if (account == null)
            {
                throw new NotFoundException(
                    "Compte bancaire introuvable.");
            }




            BankCard? card = null;


            if (complaint.RelatedBankCardId.HasValue)
            {
                card =
                    await _bankCardRepository
                    .GetByIdAsync(
                        complaint.RelatedBankCardId.Value);
            }



            return complaint.ToDetailsDto(
                account.ToInfoDto(),
                card?.ToInfoDto());

        }









        // =====================================================
        // PAGINATION
        // =====================================================


        public async Task<IReadOnlyList<ComplaintDto>> GetPagedAsync(
            int pageNumber,
            int pageSize)
        {


            var complaints =
                await _complaintRepository
                .GetPagedAsync(
                    pageNumber,
                    pageSize);



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }








        // =====================================================
        // ASSIGN AGENT
        // =====================================================


        public async Task AssignAgentAsync(
            Guid complaintId,
            AssignComplaintRequest request)
        {


            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new NotFoundException(
                    "Réclamation introuvable.");
            }




            var agent =
                await _agentRepository
                .GetByIdAsync(request.AgentId);




            if (agent == null)
            {
                throw new NotFoundException(
                    "Agent introuvable.");
            }





            complaint.AssignAgent(agent);



            _complaintRepository.Update(complaint);



            await _unitOfWork
                .SaveChangesAsync();

        }









        // =====================================================
        // CHANGE STATUS
        // =====================================================


        public async Task ChangeStatusAsync(
            Guid complaintId,
            UpdateComplaintStatusRequest request)
        {


            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new NotFoundException(
                    "Réclamation introuvable.");
            }




            complaint.ChangeStatus(
                request.Status);




            _complaintRepository.Update(
                complaint);

            // Notification client

            await _notificationService
                .NotifyStatusChangedAsync(
                    complaintId);

            await _unitOfWork
                .SaveChangesAsync();



        }

        // =====================================================
        // CHANGE PRIORITY
        // =====================================================

        public async Task ChangePriorityAsync(
            Guid complaintId,
            ChangeComplaintPriorityRequest request)
        {

            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new NotFoundException(
                    "Réclamation introuvable.");
            }



            complaint.ChangePriority(
                request.Priority);



            _complaintRepository.Update(
                complaint);



            await _unitOfWork
                .SaveChangesAsync();

        }

        // =====================================================
        // TRANSFERTOSERVICE
        // =====================================================

        public async Task TransferToServiceAsync(
            Guid complaintId,
            TransferComplaintServiceRequest request)
        {

            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new NotFoundException(
                    "Réclamation introuvable.");
            }



            complaint.TransferToService(
                request.Service);



            _complaintRepository.Update(
                complaint);



            await _unitOfWork
                .SaveChangesAsync();

        }



        // =====================================================
        // REJECT
        // =====================================================


        public async Task RejectAsync(
            Guid complaintId,
            RejectComplaintRequest request)
        {


            var complaint =
                await _complaintRepository
                .GetByIdAsync(complaintId);



            if (complaint == null)
            {
                throw new NotFoundException(
                    "Réclamation introuvable.");
            }




            complaint.Reject(
                request.Reason);



            _complaintRepository.Update(
                complaint);



            await _unitOfWork
                .SaveChangesAsync();

        }


        public async Task<ComplaintDto> GetByReferenceAsync(
             string referenceNumber)
        {

            var complaint =
                await _complaintRepository
                .GetByReferenceAsync(referenceNumber);



            if (complaint == null)
            {
                throw new NotFoundException(
                "Réclamation introuvable.");
            }



            return complaint.ToDto();

        }




        // =====================================================
        // CLIENT COMPLAINTS
        // =====================================================


        public async Task<IReadOnlyList<ComplaintDto>> GetByClientAsync(
            Guid clientId)
        {

            var complaints =
                await _complaintRepository
                .GetByClientAsync(clientId);



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }








        // =====================================================
        // AGENT COMPLAINTS
        // =====================================================


        public async Task<IReadOnlyList<ComplaintDto>> GetByAgentAsync(
            Guid agentId)
        {

            var complaints =
                await _complaintRepository
                .GetByAgentAsync(agentId);



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }




        public async Task<IReadOnlyList<ComplaintDto>> GetByCategoryAsync(
            ComplaintCategory category)
        {

            var complaints =
                await _complaintRepository
                .GetByCategoryAsync(category);



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }




        public async Task<IReadOnlyList<ComplaintDto>> GetUnassignedAsync()
        {

            var complaints =
                await _complaintRepository
                .GetUnassignedAsync();



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }



        public async Task<IReadOnlyList<ComplaintDto>> GetByStatusAsync(
            ComplaintStatus status)
        {

            var complaints =
                await _complaintRepository
                .GetByStatusAsync(status);



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }


        public async Task<IReadOnlyList<ComplaintDto>> GetByPriorityAsync(
            ComplaintPriority priority)
        {

            var complaints =
                await _complaintRepository
                .GetByPriorityAsync(priority);



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }


        public async Task<IReadOnlyList<ComplaintDto>> GetByPriorityOrderAsync()
        {

            var complaints =
                await _complaintRepository
                .GetByPriorityOrderAsync();



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }

        public async Task<IReadOnlyList<ComplaintDto>> GetOverdueAsync()
        {

            var complaints =
                await _complaintRepository
                .GetOverdueAsync();



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }



        public async Task<IReadOnlyList<ComplaintDto>> GetBySlaDeadlineAsync()
        {

            var complaints =
                await _complaintRepository
                .GetBySlaDeadlineAsync();



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }





        public async Task<IReadOnlyList<ComplaintDto>> GetHighPriorityAsync()
        {

            var complaints =
                await _complaintRepository
                .GetHighPriorityAsync();



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }



        public async Task<IReadOnlyList<ComplaintDto>> GetLatestAsync()
        {

            var complaints =
                await _complaintRepository
                .GetLatestAsync();



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }


        public async Task<IReadOnlyList<ComplaintDto>> GetOldestAsync()
        {

            var complaints =
                await _complaintRepository
                .GetOldestAsync();



            return complaints
                .Select(c => c.ToDto())
                .ToList();

        }



        // =====================================================
        // DASHBOARD
        // =====================================================


        public Task<int> CountAsync()
            => _complaintRepository.CountAsync();



        public Task<int> CountOverdueAsync()
            => _complaintRepository.CountOverdueAsync();



        public Task<int> CountUnassignedAsync()
            => _complaintRepository.CountUnassignedAsync();



        public Task<int> CountHighPriorityAsync()
            => _complaintRepository.CountHighPriorityAsync();



        public Task<int> CountByStatusAsync(
            ComplaintStatus status)
            => _complaintRepository.CountByStatusAsync(status);




        public Task<IReadOnlyList<(ComplaintCategory Category, int Count)>>
            CountByCategoryAsync()
            => _complaintRepository
                .CountComplaintsByCategoryAsync();

    }
}
