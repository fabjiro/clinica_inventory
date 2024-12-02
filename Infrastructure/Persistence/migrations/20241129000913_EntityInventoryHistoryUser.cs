using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class EntityInventoryHistoryUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "inventory_history");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "inventory_history",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_inventory_history_IdUser",
                table: "inventory_history",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_user_IdUser",
                table: "inventory_history",
                column: "IdUser",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_history_user_IdUser",
                table: "inventory_history");

            migrationBuilder.DropIndex(
                name: "IX_inventory_history_IdUser",
                table: "inventory_history");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "inventory_history");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "inventory_history",
                type: "text",
                nullable: true);
        }
    }
}
