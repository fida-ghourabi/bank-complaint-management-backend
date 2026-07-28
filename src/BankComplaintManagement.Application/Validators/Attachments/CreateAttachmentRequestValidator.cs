using BankComplaintManagement.Application.DTOs.Attachments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Attachments
{
    public class CreateAttachmentRequestValidator
     : AbstractValidator<CreateAttachmentRequest>
    {
        public CreateAttachmentRequestValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty()
                .WithMessage("Le nom du fichier est obligatoire.")
                .MaximumLength(255)
                .WithMessage("Le nom du fichier ne doit pas dépasser 255 caractères.");

            RuleFor(x => x.FilePath)
                .NotEmpty()
                .WithMessage("Le chemin du fichier est obligatoire.")
                .MaximumLength(500)
                .WithMessage("Le chemin du fichier ne doit pas dépasser 500 caractères.");

            RuleFor(x => x.ContentType)
                .NotEmpty()
                .WithMessage("Le type du fichier est obligatoire.")
                .MaximumLength(100)
                .WithMessage("Le type du fichier ne doit pas dépasser 100 caractères.");

            RuleFor(x => x.FileSize)
                .GreaterThan(0)
                .WithMessage("La taille du fichier doit être supérieure à 0.");
        }
    }
}
