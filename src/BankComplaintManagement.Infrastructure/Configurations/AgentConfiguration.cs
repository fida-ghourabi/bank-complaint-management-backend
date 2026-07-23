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
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            // ==========================================
            // Table Users (TPH)
            // ==========================================

            builder.ToTable("Users");

            // ==========================================
            // Matricule
            // ==========================================

            builder.Property(a => a.Matricule)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(a => a.Matricule)
                .IsUnique();

            // ==========================================
            // Position
            // ==========================================

            builder.Property(a => a.Position)
                .HasMaxLength(100)
                .IsRequired();

            // ==========================================
            // Service
            // ==========================================

            builder.Property(a => a.Service)
                .HasMaxLength(100)
                .IsRequired();

            // ==========================================
            // Téléphone
            // ==========================================

            builder.Property(a => a.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            // ==========================================
            // Status
            // ==========================================

            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();


        }
    }
}
