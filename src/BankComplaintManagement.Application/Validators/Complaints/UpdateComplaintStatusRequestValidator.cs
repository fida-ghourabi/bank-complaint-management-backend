using BankComplaintManagement.Application.DTOs.Complaints;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Complaints
{
    public class UpdateComplaintStatusRequestValidator
     : AbstractValidator<UpdateComplaintStatusRequest>
    {

        public UpdateComplaintStatusRequestValidator()
        {

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage(
                    "Le statut de la réclamation est obligatoire.")
                .IsInEnum()
                .WithMessage(
                    "Le statut de la réclamation est invalide.");

        }
    }
}
