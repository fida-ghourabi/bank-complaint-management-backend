using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.BankCards
{
    public class BankCardInfoDto
    {

        public Guid Id { get; set; }


        public string MaskedCardNumber { get; set; } = null!;


        public CardType Type { get; set; }


        public CardStatus Status { get; set; }


    }
}
