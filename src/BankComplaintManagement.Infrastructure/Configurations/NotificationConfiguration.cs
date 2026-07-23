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
    public class NotificationConfiguration
         : BaseEntityConfiguration<Notification>
    {


        public override void Configure(
            EntityTypeBuilder<Notification> builder)
        {


            base.Configure(builder);



            builder.ToTable("Notifications");





            // =========================
            // Enum Type
            // =========================


            builder.Property(n => n.Type)

                .HasConversion<string>()

                .HasMaxLength(50)

                .IsRequired();






            // =========================
            // Title
            // =========================


            builder.Property(n => n.Title)

                .HasMaxLength(200)

                .IsRequired();







            // =========================
            // Message
            // =========================


            builder.Property(n => n.Message)

                .HasMaxLength(1000)

                .IsRequired();







            // =========================
            // Read
            // =========================


            builder.Property(n => n.Read)

                .IsRequired();







            // =========================
            // Status Snapshot
            // =========================


            builder.Property(n => n.Status)

                .HasConversion<string>()

                .HasMaxLength(30);







            // =========================
            // User relation
            // =========================


            builder.HasOne(n => n.User)

                .WithMany(u => u.Notifications)

                .HasForeignKey(n => n.UserId)

                .OnDelete(DeleteBehavior.Restrict);








            // =========================
            // Complaint relation
            // =========================


            builder.HasOne(n => n.Complaint)

                .WithMany(c => c.Notifications)

                .HasForeignKey(n => n.ComplaintId)

                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
