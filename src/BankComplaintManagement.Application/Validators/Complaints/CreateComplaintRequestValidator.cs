using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Application.Validators.Attachments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Complaints
{
    public class CreateComplaintRequestValidator
    : AbstractValidator<CreateComplaintRequest>
    {

        public CreateComplaintRequestValidator()
        {


            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage(
                    "La catégorie de réclamation est invalide.");



            RuleFor(x => x.SubCategory)
                .NotEmpty()
                .WithMessage(
                    "La sous-catégorie est obligatoire.")
                .MaximumLength(100);



            RuleFor(x => x.RelatedBankAccountId)
                .NotEmpty()
                .WithMessage(
                    "Le compte bancaire est obligatoire.");



            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage(
                    "Le sujet est obligatoire.")
                .MaximumLength(200);



            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(
                    "La description est obligatoire.")
                .MaximumLength(5000);



            RuleFor(x => x.IncidentDate)
                .NotEmpty()
                .WithMessage(
                    "La date de l'incident est obligatoire.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(
                    "La date de l'incident ne peut pas être dans le futur.");



            RuleFor(x => x.IncidentTime)
                .NotEmpty()
                .WithMessage(
                    "L'heure de l'incident est obligatoire.");



            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage(
                    "Le lieu est obligatoire.")
                .MaximumLength(200);



            RuleFor(x => x.Channel)
                .IsInEnum()
                .WithMessage(
                    "Le canal est invalide.");



            RuleFor(x => x.BranchName)
                .MaximumLength(150)
                .When(x => !string.IsNullOrEmpty(x.BranchName));



            RuleFor(x => x.FinancialImpact)
                .GreaterThanOrEqualTo(0)
                .When(x => x.FinancialImpact.HasValue)
                .WithMessage(
                    "L'impact financier doit être positif.");



            RuleFor(x => x.Priority)
                .IsInEnum()
                .WithMessage(
                    "La priorité est invalide.");



            RuleForEach(x => x.Attachments)
                .SetValidator(new FileValidator());

        }
    }
}