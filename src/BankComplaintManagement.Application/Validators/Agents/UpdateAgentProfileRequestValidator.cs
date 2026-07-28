using BankComplaintManagement.Application.DTOs.Agents;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Agent
{
    public class UpdateAgentProfileRequestValidator
    : AbstractValidator<UpdateAgentProfileRequest>
    {

        public UpdateAgentProfileRequestValidator()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Le prénom est obligatoire.")
                .MaximumLength(100);



            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Le nom est obligatoire.")
                .MaximumLength(100);



            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("L'email est obligatoire.")
                .EmailAddress()
                .WithMessage(
                    "L'adresse email est invalide.");



            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Le numéro de téléphone est obligatoire.")
                .Matches(@"^[0-9]{8}$")
                .WithMessage(
                    "Le numéro de téléphone doit contenir 8 chiffres.");

        }
    }
}
