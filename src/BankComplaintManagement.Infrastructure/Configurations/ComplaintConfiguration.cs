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
    public class ComplaintConfiguration
           : BaseEntityConfiguration<Complaint>
    {

        public override void Configure(EntityTypeBuilder<Complaint> builder)
        {

            // Appelle la configuration commune
            base.Configure(builder);

            // =========================
            // Table
            // =========================

            builder.ToTable("Complaints");

            // =========================
            // Identification
            // =========================


            builder.Property(c => c.ReferenceNumber)
                .HasMaxLength(50)
                .IsRequired();


            builder.HasIndex(c => c.ReferenceNumber)
                .IsUnique();

            // =========================
            // Classification
            // =========================

            builder.Property(c => c.Category)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();


            builder.Property(c => c.SubCategory)
                .HasMaxLength(100)
                .IsRequired();

            // =========================
            // Description
            // =========================

            builder.Property(c => c.Subject)
                .HasMaxLength(200)
                .IsRequired();


            builder.Property(c => c.Description)
                .IsRequired();

            // =========================
            // Incident Information
            // =========================


            builder.Property(c => c.IncidentDate)
                .IsRequired();


            builder.Property(c => c.IncidentTime)
                .IsRequired();


            builder.Property(c => c.Location)
                .HasMaxLength(200)
                .IsRequired();


            builder.Property(c => c.Channel)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();


            builder.Property(c => c.BranchName)
                .HasMaxLength(150);

            // =========================
            // Financial Impact
            // =========================

            builder.Property(c => c.FinancialImpact)
                .HasPrecision(18, 2);


            // =========================
            // Workflow
            // =========================

            builder.Property(c => c.Priority)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();


            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            // =========================
            // Transfer
            // =========================


            builder.Property(c => c.TransferTo)
                .HasConversion<string>()
                .HasMaxLength(100);


            builder.Property(c => c.RejectionReason)
                .HasMaxLength(500);




            builder.Property(c => c.Unread)
                .IsRequired();


            builder.Property(c => c.SlaDueDate)
                .IsRequired();



            // Index

            builder.HasIndex(c => c.ClientId);

            builder.HasIndex(c => c.Status);

            // Relations

            // =========================
            // Client relationship
            // =========================

            builder.HasOne(c => c.Client)
                .WithMany(c => c.Complaints)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Agent relationship
            // =========================

            builder.HasOne(c => c.AssignedAgent)
                .WithMany(a => a.AssignedComplaints)
                .HasForeignKey(c => c.AssignedAgentId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
