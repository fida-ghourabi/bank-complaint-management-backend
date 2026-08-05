using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class Client : User
    {

        public string CustomerNumber { get; private set; } = null!;
        public string CIN { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;


        public ClientStatus Status { get; set; }



        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();



        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();



        private Client()
        {

        }



        public Client(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            string cin,
            string phoneNumber
            )
            : base(
                firstName,
                lastName,
                email,
                passwordHash,
                UserRole.Client)
        {
            CustomerNumber = GenerateCustomerNumber();


            CIN = cin;

            PhoneNumber = phoneNumber;

            Status = ClientStatus.actif;

        }



        private string GenerateCustomerNumber()
        {
            return $"CLI-{DateTime.UtcNow.Year}-{Random.Shared.Next(100000, 999999)}";
        }

        //Un client bloqué ne doit pas créer une réclamation.
        public void EnsureCanCreateComplaint()
        {
            if (Status == ClientStatus.bloque)
            {
                throw new InvalidOperationException(
                    "Le client est bloqué et ne peut pas créer de réclamation.");
            }
        }

    }
}
