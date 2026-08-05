using BankComplaintManagement.Application.DTOs.Clients;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Client
{
    public class ChangePasswordRequestValidator
    : AbstractValidator<ClientChangePasswordRequest>
    {

        public ChangePasswordRequestValidator()
        {

            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage(
                    "L'ancien mot de passe est obligatoire.");



            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(
                    "Le nouveau mot de passe est obligatoire.")
                .MinimumLength(8)
                .WithMessage(
                    "Le nouveau mot de passe doit contenir au minimum 8 caractères.");



            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword)
                .WithMessage(
                    "La confirmation du mot de passe est incorrecte.");


            RuleFor(x => x)
                .Must(x => x.OldPassword != x.NewPassword)
                .WithMessage(
                    "Le nouveau mot de passe doit être différent de l'ancien.");

        }
    }
}