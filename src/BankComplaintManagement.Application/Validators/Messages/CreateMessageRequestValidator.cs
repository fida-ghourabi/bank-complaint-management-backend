using BankComplaintManagement.Application.DTOs.Messages;
using BankComplaintManagement.Application.Validators.Attachments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Messages
{
    public class CreateMessageRequestValidator
    : AbstractValidator<CreateMessageRequest>
    {
        public CreateMessageRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Le message est obligatoire.")
                .MaximumLength(2000)
                .WithMessage("Le message ne doit pas dépasser 2000 caractères.");

            RuleForEach(x => x.Attachments)
                .SetValidator(new FileValidator());
        }
    }
}
