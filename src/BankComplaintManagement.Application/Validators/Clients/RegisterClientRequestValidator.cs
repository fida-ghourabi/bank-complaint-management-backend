using BankComplaintManagement.Application.DTOs.Clients;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Client
{
    public class RegisterClientRequestValidator
    : AbstractValidator<RegisterClientRequest>
    {

        public RegisterClientRequestValidator()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Le prénom est obligatoire.")
                .MaximumLength(100)
                .WithMessage("Le prénom ne doit pas dépasser 100 caractères.");



            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Le nom est obligatoire.")
                .MaximumLength(100)
                .WithMessage("Le nom ne doit pas dépasser 100 caractères.");



            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("L'email est obligatoire.")
                .EmailAddress()
                .WithMessage("L'adresse email est invalide.");



            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Le mot de passe est obligatoire.")
                .MinimumLength(8)
                .WithMessage(
                    "Le mot de passe doit contenir au minimum 8 caractères.");



            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage(
                    "La confirmation du mot de passe ne correspond pas.");



            RuleFor(x => x.CIN)
                .NotEmpty()
                .WithMessage("Le CIN est obligatoire.")
                .Length(8)
                .WithMessage(
                    "Le CIN doit contenir exactement 8 caractères.");



            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Le numéro de téléphone est obligatoire.")
                .Matches(@"^[0-9]{8}$")
                .WithMessage(
                    "Le numéro de téléphone doit contenir 8 chiffres.");
        }

    }

}