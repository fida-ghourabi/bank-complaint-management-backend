using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IComplaintService
    {


        // =============================
        // Query
        // =============================


        Task<ComplaintDetailsDto?> GetDetailsAsync(
            Guid complaintId);



        Task<ComplaintDto?> GetByIdAsync(
            Guid complaintId);



        Task<IReadOnlyList<ComplaintDto>> GetPagedAsync(
            int pageNumber,
            int pageSize);

        Task<ComplaintDto?> GetByReferenceAsync(
            string referenceNumber);

        Task<IReadOnlyList<ComplaintDto>> GetByClientAsync(
            Guid clientId);



        Task<IReadOnlyList<ComplaintDto>> GetByAgentAsync(
            Guid agentId);



        Task<IReadOnlyList<ComplaintDto>> GetByCategoryAsync(
           ComplaintCategory category);

        Task<IReadOnlyList<ComplaintDto>> GetUnassignedAsync();

        Task<IReadOnlyList<ComplaintDto>> GetByStatusAsync(
            ComplaintStatus status);

        Task<IReadOnlyList<ComplaintDto>> GetByPriorityAsync(
            ComplaintPriority priority);

        Task<IReadOnlyList<ComplaintDto>> GetByPriorityOrderAsync();

        Task<IReadOnlyList<ComplaintDto>> GetOverdueAsync();

        Task<IReadOnlyList<ComplaintDto>> GetBySlaDeadlineAsync();

        Task<IReadOnlyList<ComplaintDto>> GetHighPriorityAsync();


        Task<IReadOnlyList<ComplaintDto>> GetLatestAsync();

        Task<IReadOnlyList<ComplaintDto>> GetOldestAsync();

        // =============================
        // Command
        // =============================


        Task<ComplaintDto> CreateAsync(
            Guid clientId,
            CreateComplaintRequest request);



        Task AssignAgentAsync(
            Guid complaintId,
            AssignComplaintRequest request);



        Task ChangeStatusAsync(
            Guid complaintId,
            UpdateComplaintStatusRequest request);



        Task RejectAsync(
            Guid complaintId,
            RejectComplaintRequest request);




        // =============================
        // Dashboard
        // =============================


        Task<int> CountAsync();



        Task<int> CountOverdueAsync();



        Task<int> CountUnassignedAsync();



        Task<int> CountHighPriorityAsync();



        Task<int> CountByStatusAsync(
            ComplaintStatus status);



        Task<IReadOnlyList<(ComplaintCategory Category, int Count)>>
            CountByCategoryAsync();

    }
}
