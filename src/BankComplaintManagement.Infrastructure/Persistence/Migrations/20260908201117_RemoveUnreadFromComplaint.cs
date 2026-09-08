using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankComplaintManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnreadFromComplaint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unread",
                table: "Complaints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Unread",
                table: "Complaints",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
