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
    public class RefreshTokenConfiguration
       : BaseEntityConfiguration<RefreshToken>
    {


        public override void Configure(
            EntityTypeBuilder<RefreshToken> builder)
        {

            base.Configure(builder);



            // ============================
            // Table
            // ============================

            builder.ToTable("RefreshTokens");




            // ============================
            // Primary Key
            // ============================

            builder.HasKey(r => r.Id);





            // ============================
            // Token
            // ============================

            builder.Property(r => r.Token)

                .IsRequired()

                .HasMaxLength(500);




            builder.HasIndex(r => r.Token)

                .IsUnique();





            // ============================
            // Expiration
            // ============================

            builder.Property(r => r.ExpiresAt)

                .IsRequired();






            // ============================
            // Revoked
            // ============================

            builder.Property(r => r.IsRevoked)

                .IsRequired();





            // ============================
            // User Relation
            // ============================

            builder.HasOne(r => r.User)

                .WithMany(u => u.RefreshTokens)

                .HasForeignKey(r => r.UserId)

                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
