using BankComplaintManagement.Application.DTOs.Complaints;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Complaints
{
    public class ChangeComplaintPriorityRequestValidator
     : AbstractValidator<ChangeComplaintPriorityRequest>
    {

        public ChangeComplaintPriorityRequestValidator()
        {

            RuleFor(x => x.Priority)
                .NotEmpty()
                .WithMessage(
                    "La priorité est obligatoire.")
                .IsInEnum()
                .WithMessage(
                    "La priorité est invalide.");

        }
    }
}