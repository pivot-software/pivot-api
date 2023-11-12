using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAddedFieldsAboutNofications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "can_send_email",
                table: "users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "can_send_sms",
                table: "users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "can_send_system_notification",
                table: "users",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "can_send_email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "can_send_sms",
                table: "users");

            migrationBuilder.DropColumn(
                name: "can_send_system_notification",
                table: "users");
        }
    }
}
