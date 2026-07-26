using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Agents
{
    public class AgentDto
    {

        public Guid Id { get; set; }


        public string Matricule { get; set; } = null!;


        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;


        public AgentStatus Status { get; set; }



        // Statistiques agent

        public int AssignedComplaints { get; set; }


        public int InProgressComplaints { get; set; }


        public double ResolutionRate { get; set; }

    }
}
