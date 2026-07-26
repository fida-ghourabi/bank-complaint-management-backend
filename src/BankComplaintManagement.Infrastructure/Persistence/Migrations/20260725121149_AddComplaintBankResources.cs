using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankComplaintManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddComplaintBankResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RelatedBankAccountId",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedBankCardId",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedBankAccountId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "RelatedBankCardId",
                table: "Complaints");
        }
    }
}
