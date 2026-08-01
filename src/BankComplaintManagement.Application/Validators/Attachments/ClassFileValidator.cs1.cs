using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Validators.Attachments
{
    public class FileValidator
        : AbstractValidator<IFormFile>
    {

        private readonly string[] _allowedExtensions =
        {
            ".pdf",
            ".png",
            ".jpg",
            ".jpeg"
        };


        public FileValidator()
        {

            RuleFor(x => x)
                .NotNull()
                .WithMessage(
                "Le fichier est obligatoire.");



            RuleFor(x => x.Length)
                .LessThanOrEqualTo(
                    5 * 1024 * 1024)
                .WithMessage(
                "La taille maximale est 5 MB.");



            RuleFor(x => x.FileName)
                .Must(HaveValidExtension)
                .WithMessage(
                "Type de fichier non autorisé.");

        }



        private bool HaveValidExtension(
            string fileName)
        {

            var extension =
                Path.GetExtension(fileName)
                .ToLower();


            return _allowedExtensions
                .Contains(extension);

        }

    }
}
