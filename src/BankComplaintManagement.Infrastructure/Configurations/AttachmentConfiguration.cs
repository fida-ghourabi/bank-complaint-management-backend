using Microsoft.EntityFrameworkCore;
using BankComplaintManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Configurations
{
    public class AttachmentConfiguration
         : BaseEntityConfiguration<Attachment>
    {


        public override void Configure(
            EntityTypeBuilder<Attachment> builder)
        {

            base.Configure(builder);



            builder.ToTable("Attachments");




            // =========================
            // File Name
            // =========================


            builder.Property(a => a.FileName)

                .HasMaxLength(255)

                .IsRequired();





            // =========================
            // File Path
            // =========================


            builder.Property(a => a.FilePath)

                .HasMaxLength(500)

                .IsRequired();






            // =========================
            // Content Type
            // =========================


            builder.Property(a => a.ContentType)

                .HasMaxLength(100)

                .IsRequired();






            // =========================
            // File Size
            // =========================


            builder.Property(a => a.FileSize)

                .IsRequired();







            // =========================
            // Complaint relation
            // =========================


            builder.HasOne(a => a.Complaint)

                .WithMany(c => c.Attachments)

                .HasForeignKey(a => a.ComplaintId)

                .OnDelete(DeleteBehavior.Cascade);







            // =========================
            // Message relation
            // =========================


            builder.HasOne(a => a.Message)

                .WithMany(m => m.Attachments)

                .HasForeignKey(a => a.MessageId)

                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
