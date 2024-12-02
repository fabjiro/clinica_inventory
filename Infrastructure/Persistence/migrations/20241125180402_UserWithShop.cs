using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class UserWithShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_ShopId",
                table: "user",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_shop_ShopId",
                table: "user",
                column: "ShopId",
                principalTable: "shop",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_shop_ShopId",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_ShopId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "user");
        }
    }
}
