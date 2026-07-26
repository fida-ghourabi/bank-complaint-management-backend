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
    public class BankAccountConfiguration
       : BaseEntityConfiguration<BankAccount>
    {

        public override void Configure(
            EntityTypeBuilder<BankAccount> builder)
        {

            base.Configure(builder);


            // =========================
            // Table
            // =========================

            builder.ToTable("BankAccounts");



            // =========================
            // Account Number
            // =========================

            builder.Property(b => b.AccountNumber)
                .HasMaxLength(50)
                .IsRequired();



            builder.HasIndex(b => b.AccountNumber)
                .IsUnique();



            // =========================
            // IBAN
            // =========================

            builder.Property(b => b.IBAN)
                .HasMaxLength(34)
                .IsRequired();



            builder.HasIndex(b => b.IBAN)
                .IsUnique();




            // =========================
            // Account Type
            // =========================

            builder.Property(b => b.Type)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();



            builder.Property(x => x.Balance)
                .HasPrecision(18, 2);


            // =========================
            // Client Relationship
            // =========================


            builder.HasOne(b => b.Client)

                .WithMany(c => c.BankAccounts)

                .HasForeignKey(b => b.ClientId)

                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
