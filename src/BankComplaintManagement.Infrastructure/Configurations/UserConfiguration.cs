using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            // Configuration commune (Id, CreatedAt, UpdatedAt)
            base.Configure(builder);

            // ================================
            // Table
            // ================================
            builder.ToTable("Users");

            // ================================
            // Clé primaire
            // ================================
            builder.HasKey(u => u.Id);

            // ================================
            // Propriétés communes
            // ================================
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

           

            // ================================
            // Enum Role
            // Stocké sous forme de texte
            // ================================
            builder.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            // ================================
            // Héritage (TPH)
            // Le discriminant est directement
            // la propriété Role
            // ================================
            builder.HasDiscriminator(u => u.Role)
                .HasValue<Client>(UserRole.Client)
                .HasValue<Agent>(UserRole.Agent)
                .HasValue<Admin>(UserRole.Admin);




        }
    }
}

