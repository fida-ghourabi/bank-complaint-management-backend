using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class Agent : User
    {


        public string Matricule { get; private set; } = null!;


        public string Position { get; set; } = null!;


        public string Service { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;


        public AgentStatus Status { get; set; }



        public ICollection<Complaint> AssignedComplaints { get; set; } = new List<Complaint>();



        private Agent()
        {
        }



        public Agent(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            string matricule,
            string position,
            string service,
            string phoneNumber
        )
        : base(
            firstName,
            lastName,
            email,
            passwordHash,
            UserRole.Agent)
        {

            Matricule = matricule;

            Position = position;

            Service = service;

            PhoneNumber = phoneNumber;

            Status = AgentStatus.Active;

        }




        public void EnsureCanReceiveComplaint()
        {
            if (Status != AgentStatus.Active)
            {
                throw new InvalidOperationException(
                    "Cet agent n'est pas actif et ne peut pas recevoir de réclamation.");
            }
        }
    }
}
