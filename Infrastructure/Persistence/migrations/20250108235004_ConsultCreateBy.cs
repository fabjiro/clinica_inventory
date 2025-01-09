using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class ConsultCreateBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatedById",
                table: "consult",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_consult_UserCreatedById",
                table: "consult",
                column: "UserCreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_consult_user_UserCreatedById",
                table: "consult",
                column: "UserCreatedById",
                principalTable: "user",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consult_user_UserCreatedById",
                table: "consult");

            migrationBuilder.DropIndex(
                name: "IX_consult_UserCreatedById",
                table: "consult");

            migrationBuilder.DropColumn(
                name: "UserCreatedById",
                table: "consult");
        }
    }
}
