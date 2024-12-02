using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class CategoryWithShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "category",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_ShopId",
                table: "category",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_category_shop_ShopId",
                table: "category",
                column: "ShopId",
                principalTable: "shop",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_shop_ShopId",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_category_ShopId",
                table: "category");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "category");
        }
    }
}
