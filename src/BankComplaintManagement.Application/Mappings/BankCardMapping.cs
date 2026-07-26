using BankComplaintManagement.Application.DTOs.BankCards;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class BankCardMapping
    {

        public static BankCardInfoDto ToInfoDto(
            this BankCard card)
        {

            return new BankCardInfoDto
            {

                Id = card.Id,


                MaskedCardNumber =
                    MaskCardNumber(card.CardNumber),


                Type =
                    card.CardType,


                Status =
                    card.Status

            };

        }




        private static string MaskCardNumber(
            string cardNumber)
        {

            if (string.IsNullOrEmpty(cardNumber)
                || cardNumber.Length < 4)
            {
                return cardNumber;
            }


            return
                $"**** **** **** {cardNumber.Substring(cardNumber.Length - 4)}";

        }

    }
}
