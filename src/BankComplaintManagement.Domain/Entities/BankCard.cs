using BankComplaintManagement.Domain.Common;
using BankComplaintManagement.Domain.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
  
    public class BankCard : BaseEntity
    {


        public string CardNumber { get; set; } = null!;

        public CardType CardType { get; set; }

        public DateTime ExpirationDate { get; set; }    


        public CardStatus Status { get; set; }



        // Relation avec le compte bancaire

        public Guid BankAccountId { get; private set; }


        public BankAccount BankAccount { get; private set; } = null!;




        private BankCard()
        {

        }


        public BankCard(
            string cardNumber,
            CardType cardType,
            DateTime expirationDate,
            Guid bankAccountId)
        {

            CardNumber = cardNumber;

            CardType = cardType;

            ExpirationDate = expirationDate;

            Status = CardStatus.Active;

            BankAccountId = bankAccountId;

        }

    }
}
