using BankComplaintManagement.Application.DTOs.Complaints;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Complaints
{
    public class RejectComplaintRequestValidator
    : AbstractValidator<RejectComplaintRequest>
    {

        public RejectComplaintRequestValidator()
        {

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage(
                    "Le motif du rejet est obligatoire.")
                .MaximumLength(500)
                .WithMessage(
                    "Le motif ne doit pas dépasser 500 caractères.");

        }
    }
}
