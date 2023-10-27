using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Api.Migrations
{
    /// <inheritdoc />
    public partial class Workspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workspaces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_name = table.Column<string>(type: "text", nullable: false),
                    business_logo = table.Column<string>(type: "text", nullable: false),
                    business_color = table.Column<string>(type: "text", nullable: false),
                    template_mode = table.Column<char>(type: "character(1)", nullable: false),
                    admin_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspaces", x => x.id);
                    table.ForeignKey(
                        name: "FK_workspaces_users_admin_id",
                        column: x => x.admin_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_workspaces_admin_id",
                table: "workspaces",
                column: "admin_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "workspaces");
        }
    }
}
