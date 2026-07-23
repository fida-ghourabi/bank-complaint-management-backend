using BankComplaintManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Configurations
{
    public class MessageConfiguration
        : BaseEntityConfiguration<Message>
    {


        public override void Configure(
            EntityTypeBuilder<Message> builder)
        {


            // Configuration commune
            // Id
            // CreatedAt
            // UpdatedAt

            base.Configure(builder);




            // =========================
            // Table
            // =========================

            builder.ToTable("Messages");





            // =========================
            // Text
            // =========================


            builder.Property(m => m.Text)

                .HasMaxLength(2000)

                .IsRequired();






            // =========================
            // Is Agent
            // =========================


            builder.Property(m => m.IsAgent)

                .IsRequired();






            // =========================
            // Complaint Relationship
            // =========================


            builder.HasOne(m => m.Complaint)


                .WithMany(c => c.Messages)


                .HasForeignKey(m => m.ComplaintId)


                .OnDelete(DeleteBehavior.Cascade);

        }

    }

}
