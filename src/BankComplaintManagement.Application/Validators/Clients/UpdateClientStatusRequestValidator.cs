using BankComplaintManagement.Application.DTOs.Clients;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Client
{
    public class UpdateClientStatusRequestValidator
    : AbstractValidator<UpdateClientStatusRequest>
    {

        public UpdateClientStatusRequestValidator()
        {

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(
                    "Le statut client est invalide.");

        }
    }
}
