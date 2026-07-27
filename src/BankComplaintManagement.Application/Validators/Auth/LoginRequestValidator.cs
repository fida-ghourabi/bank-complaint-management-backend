using BankComplaintManagement.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Auth
{
    public class LoginRequestValidator
     : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("L'email est obligatoire.")
                .EmailAddress()
                .WithMessage("L'adresse email est invalide.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Le mot de passe est obligatoire.");
        }
    }
}