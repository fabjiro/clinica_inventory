using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class EntityInventoryHistoryShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdShop",
                table: "inventory_history",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_inventory_history_IdShop",
                table: "inventory_history",
                column: "IdShop");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_shop_IdShop",
                table: "inventory_history",
                column: "IdShop",
                principalTable: "shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_history_shop_IdShop",
                table: "inventory_history");

            migrationBuilder.DropIndex(
                name: "IX_inventory_history_IdShop",
                table: "inventory_history");

            migrationBuilder.DropColumn(
                name: "IdShop",
                table: "inventory_history");
        }
    }
}
