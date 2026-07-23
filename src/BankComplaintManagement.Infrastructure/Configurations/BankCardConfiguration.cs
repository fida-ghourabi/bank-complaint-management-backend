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
    public class BankCardConfiguration
        : BaseEntityConfiguration<BankCard>
    {

        public override void Configure(
            EntityTypeBuilder<BankCard> builder)
        {

            // Configuration commune :
            // Id, CreatedAt, UpdatedAt
            base.Configure(builder);



            // =========================
            // Table
            // =========================

            builder.ToTable("BankCards");



            // =========================
            // Card Number
            // =========================

            builder.Property(c => c.CardNumber)
                .HasMaxLength(20)
                .IsRequired();


            // Une carte bancaire possède un numéro unique

            builder.HasIndex(c => c.CardNumber)
                .IsUnique();





            // =========================
            // Card Type
            // =========================

            builder.Property(c => c.CardType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();





            // =========================
            // Card Status
            // =========================

            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();





            // =========================
            // Expiration Date
            // =========================

            builder.Property(c => c.ExpirationDate)
                .IsRequired();





            // =========================
            // Relation BankAccount
            // =========================


            builder.HasOne(c => c.BankAccount)

                .WithMany(a => a.BankCards)

                .HasForeignKey(c => c.BankAccountId)

                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
