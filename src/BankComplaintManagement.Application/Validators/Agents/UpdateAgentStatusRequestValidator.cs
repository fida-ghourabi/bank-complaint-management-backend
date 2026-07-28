using BankComplaintManagement.Application.DTOs.Agents;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Agent
{
    public class UpdateAgentStatusRequestValidator
     : AbstractValidator<UpdateAgentStatusRequest>
    {

        public UpdateAgentStatusRequestValidator()
        {

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(
                    "Le statut agent est invalide.");

        }
    }
}
