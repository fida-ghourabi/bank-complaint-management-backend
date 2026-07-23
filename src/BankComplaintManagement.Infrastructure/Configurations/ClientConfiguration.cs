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
    public class ClientConfiguration
           : IEntityTypeConfiguration<Client>
    {

        public void Configure(EntityTypeBuilder<Client> builder)
        {

            // =====================================
            // Table héritée User (TPH)
            // =====================================
            builder.ToTable("Users");



            // =====================================
            // Propriétés spécifiques Client
            // =====================================

            builder.Property(c => c.CustomerNumber)
                 .HasMaxLength(20)
                 .IsRequired();


            builder.HasIndex(c => c.CustomerNumber)
                .IsUnique();


            builder.Property(c => c.CIN)
                .HasMaxLength(8)
                .IsRequired();



            builder.HasIndex(c => c.CIN)
                .IsUnique();



            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();



            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();










        }
    }
}
