using BankComplaintManagement.Application.DTOs.Complaints;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Complaints
{
    public class TransferComplaintServiceRequestValidator
     : AbstractValidator<TransferComplaintServiceRequest>
    {

        public TransferComplaintServiceRequestValidator()
        {

            RuleFor(x => x.Service)
                .NotEmpty()
                .WithMessage(
                    "Le service est obligatoire.")
                .IsInEnum()
                .WithMessage(
                    "Le service de transfert est invalide.");

        }
    }
}
