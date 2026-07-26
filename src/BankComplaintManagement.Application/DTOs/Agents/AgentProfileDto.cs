using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Agents
{
    public class AgentProfileDto
    {

        public Guid Id { get; set; }


        public string Matricule { get; set; } = null!;


        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;

        public string Position { get; set; } = null!;


        public string Service { get; set; } = null!;
        public DateTime CreatedAt { get; set; }


        // statistiques

        public int ProcessedComplaints { get; set; }


        public int InProgressComplaints { get; set; }


        public int OverdueComplaints { get; set; }


        public double ResolutionRate { get; set; }

    }
}
