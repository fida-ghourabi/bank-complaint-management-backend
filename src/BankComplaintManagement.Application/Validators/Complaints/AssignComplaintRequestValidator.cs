using BankComplaintManagement.Application.DTOs.Complaints;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Complaints
{
    public class AssignComplaintRequestValidator
     : AbstractValidator<AssignComplaintRequest>
    {

        public AssignComplaintRequestValidator()
        {

            RuleFor(x => x.AgentId)
                .NotEmpty()
                .WithMessage(
                    "L'agent est obligatoire.");

        }
    }
}
